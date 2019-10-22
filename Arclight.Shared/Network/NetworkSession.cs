using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Arclight.Database.Auth.Model;
using Arclight.Shared.Cryptography;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Packet;
using NLog;

namespace Arclight.Shared.Network
{
    public class Session : IUpdate
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public AccountModel Account { get; private set; }

        private SessionState state;

        private Socket socket;
        readonly byte[] buffer = new byte[4096];

        private PacketOnDeck onDeck;
        private readonly ConcurrentQueue<ClientPacket> incomingPackets = new ConcurrentQueue<ClientPacket>();
        private readonly Queue<ServerPacket> outgoingPackets = new Queue<ServerPacket>();

        public virtual void Update(double lastTick)
        {
            // process pending packet queue
            while (incomingPackets.TryDequeue(out ClientPacket packet))
                ProcessPacket(packet);

            // flush pending packet queue
            while (outgoingPackets.TryDequeue(out ServerPacket packet))
                FlushPacket(packet);
        }

        /// <summary>
        /// Initialise <see cref="Session"/> with new <see cref="Socket"/> and begin listening for data.
        /// </summary>
        public void Accept(Socket newSocket)
        {
            if (socket != null)
                throw new InvalidOperationException("Tried to initialise an existing session which already has a socket!");

            socket = newSocket;
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnReceiveData, null);

            log.Trace("New client connected.");
        }

        /// <summary>
        /// Authenticate <see cref="Session"/> with a new <see cref="AccountModel"/>.
        /// </summary>
        public void Authenticate(AccountModel account)
        {
            if (Account != null)
                throw new InvalidOperationException();

            Account = account;
            state   = SessionState.Authenticated;
        }

        /// <summary>
        /// Enqueue <see cref="IWritable"/> to be sent to the client.
        /// </summary>
        public void SendMessage(IWritable message)
        {
            MessageOpcode? opcode = MessageManager.Instance.GetMessageOpcode(message);
            if (opcode == null)
                throw new ArgumentException("Tried to send message with no attribute!");

            ushort seed = (ushort)RandomProvider.RandomInteger(0, 24);
            var packet = new ServerPacket(opcode.Value, seed, message);
            outgoingPackets.Enqueue(packet);

            log.Trace($"Send message {opcode}(0x{opcode:X}).");
        }

        protected virtual void Disconnect()
        {
            incomingPackets.Clear();
            outgoingPackets.Clear();
        }

        private void OnReceiveData(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);
                if (length == 0)
                {
                    Disconnect();
                    return;
                }

                using var stream = new MemoryStream(buffer, 0, length);
                using var reader = new BinaryReader(stream);

                while (stream.Remaining() != 0)
                {
                    // no packet on deck waiting for additional information, new data will be part of a new packet
                    if (onDeck == null)
                    {
                        int remaining = stream.Remaining();
                        if (remaining < PacketHeader.Size)
                        {
                            // wait for remaining header to arrive before reading
                            socket.BeginReceive(buffer, remaining, buffer.Length - remaining, SocketFlags.None, OnReceiveData, null);
                            return;
                        }

                        var header = new PacketHeader();
                        header.Read(reader);
                        onDeck = new PacketOnDeck(header);
                    }

                    onDeck.Buffer.Populate(reader);
                    if (onDeck.Buffer.IsComplete)
                    {
                        var packet = new ClientPacket(onDeck.Header, onDeck.Buffer.Data);

                        incomingPackets.Enqueue(packet);
                        onDeck = null;
                    }
                }

                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnReceiveData, null);
            }
            catch (Exception exception)
            {
                log.Error(exception);
                Disconnect();
            }
        }

        private void ProcessPacket(ClientPacket packet)
        {
            using var stream = new PacketCryptoStream(packet.Header.Seed, packet.Data);
            using var reader = new BinaryReader(stream);

            byte group = reader.ReadByte();
            byte index = reader.ReadByte();
            MessageOpcode opcode = (MessageOpcode)((group << 8) | index);

            IReadable message = MessageManager.Instance.GetMessage(opcode);
            if (message == null)
            {
                log.Warn($"Received unknown message {opcode}(0x{opcode:X})!");
                return;
            }

            (MessageHandlerAttribute attribute, MessageHandlerDelegate @delegate) = MessageManager.Instance.GetMessageInformation(opcode);
            if (attribute == null)
            {
                log.Warn($"Received unhandled message {opcode}(0x{opcode:X})!");
                return;
            }

            if (state != attribute.State)
            {
                log.Warn($"Received message {opcode}(0x{opcode:X}) while in an invalid state!");
                return;
            }

            log.Trace($"Received message {opcode}(0x{opcode:X}).");

            message.Read(reader);
            if (reader.BaseStream.Remaining() > 0)
                log.Warn($"Failed to read entire contents of message {opcode}(0x{opcode:X})!");

            try
            {
                @delegate.Invoke(this, message);
            }
            catch (Exception exception)
            {
                log.Error(exception);
            }
        }

        private void FlushPacket(ServerPacket packet)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            packet.Header.Write(writer);
            writer.Write(packet.Data);

            socket.Send(stream.ToArray(), SocketFlags.None);
        }
    }
}
