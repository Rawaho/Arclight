using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NLog;

namespace Arclight.Shared.Network.Message
{
    public delegate void MessageHandlerDelegate(Session session, IReadable message);

    public sealed class MessageManager : Singleton<MessageManager>
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private delegate IReadable MessageFactoryDelegate();
        private ImmutableDictionary<MessageOpcode, MessageFactoryDelegate> clientMessageFactories;
        private ImmutableDictionary<Type, MessageOpcode> serverMessageOpcodes;

        private ImmutableDictionary<MessageOpcode, (MessageHandlerAttribute, MessageHandlerDelegate)> clientMessageHandlers;

        public void Initialise()
        {
            InitialiseMessages();
            InitialiseMessageHandlers();
        }

        private void InitialiseMessages()
        {
            var messageFactories = ImmutableDictionary.CreateBuilder<MessageOpcode, MessageFactoryDelegate>();
            var messageOpcodes   = ImmutableDictionary.CreateBuilder<Type, MessageOpcode>();

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()
                .Concat(Assembly.GetEntryAssembly().GetTypes()))
            {
                MessageAttribute attribute = type.GetCustomAttribute<MessageAttribute>();
                if (attribute == null)
                    continue;

                if (typeof(IReadable).IsAssignableFrom(type))
                {
                    NewExpression @new = Expression.New(type.GetConstructor(Type.EmptyTypes));
                    messageFactories.Add(attribute.Opcode, Expression.Lambda<MessageFactoryDelegate>(@new).Compile());
                }
                if (typeof(IWritable).IsAssignableFrom(type))
                    messageOpcodes.Add(type, attribute.Opcode);
            }

            clientMessageFactories = messageFactories.ToImmutable();
            serverMessageOpcodes   = messageOpcodes.ToImmutable();

            log.Info($"Initialised {clientMessageFactories.Count + serverMessageOpcodes.Count} message(s).");
        }

        private void InitialiseMessageHandlers()
        {
            var messageHandlers = ImmutableDictionary.CreateBuilder<MessageOpcode, (MessageHandlerAttribute, MessageHandlerDelegate)>();

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()
                .Concat(Assembly.GetEntryAssembly().GetTypes()))
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    MessageHandlerAttribute attribute = method.GetCustomAttribute<MessageHandlerAttribute>();
                    if (attribute == null)
                        continue;

                    ParameterInfo[] parameterInfo = method.GetParameters();

                    #region Debug
                    Debug.Assert(parameterInfo.Length == 2);
                    Debug.Assert(typeof(Session).IsAssignableFrom(parameterInfo[0].ParameterType));
                    Debug.Assert(typeof(IReadable).IsAssignableFrom(parameterInfo[1].ParameterType));
                    #endregion

                    ParameterExpression sessionParameter = Expression.Parameter(typeof(Session));
                    ParameterExpression messageParameter = Expression.Parameter(typeof(IReadable));

                    MethodCallExpression call = Expression.Call(method,
                        Expression.Convert(sessionParameter, parameterInfo[0].ParameterType),
                        Expression.Convert(messageParameter, parameterInfo[1].ParameterType));

                    Expression<MessageHandlerDelegate> lambda =
                        Expression.Lambda<MessageHandlerDelegate>(call, sessionParameter, messageParameter);

                    messageHandlers.Add(attribute.Opcode, (attribute, lambda.Compile()));
                }
            }

            clientMessageHandlers = messageHandlers.ToImmutable();
            log.Info($"Initialised {clientMessageHandlers.Count} message handler(s).");
        }

        /// <summary>
        /// Return <see cref="IReadable"/> for <see cref="MessageOpcode"/>.
        /// </summary>
        public IReadable GetMessage(MessageOpcode opcode)
        {
            return clientMessageFactories.TryGetValue(opcode, out MessageFactoryDelegate factory) ? factory.Invoke() : null;
        }

        /// <summary>
        /// Return <see cref="MessageHandlerAttribute"/> and <see cref="MessageHandlerDelegate"/> as a <see cref="ValueTuple"/> for <see cref="MessageOpcode"/>. 
        /// </summary>
        public (MessageHandlerAttribute attribute, MessageHandlerDelegate @delegate) GetMessageInformation(MessageOpcode opcode)
        {
            clientMessageHandlers.TryGetValue(opcode, out (MessageHandlerAttribute attribute, MessageHandlerDelegate @delegate) handler);
            return handler;
        }

        /// <summary>
        /// Return <see cref="MessageOpcode"/> for <see cref="IWritable"/>.
        /// </summary>
        public MessageOpcode? GetMessageOpcode(IWritable message)
        {
            if (!serverMessageOpcodes.TryGetValue(message.GetType(), out MessageOpcode opcode))
                return null;
            return opcode;
        }
    }
}
