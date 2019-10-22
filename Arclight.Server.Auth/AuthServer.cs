using System;
using System.IO;
using System.Reflection;
using Arclight.Shared.Command;
using Arclight.Shared.Configuration;
using Arclight.Shared.Database;
using Arclight.Shared.Game;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;
using NLog;

namespace Arclight.Server.Auth
{
    internal static class AuthServer
    {
        #if DEBUG
        private const string Title = "Arclight Login Server (DEBUG)";
        #else
        private const string Title = "Arclight Login Server (RELEASE)";
        #endif

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            Console.Title = Title;
            log.Info("Initialising...");

            CommandManager.Instance.Initialise();

            ConfigurationManager<AuthServerConfiguration>.Instance.Initialise("AuthServer.json");
            AuthServerConfiguration configuration = ConfigurationManager<AuthServerConfiguration>.Instance.Model;

            DatabaseManager.Instance.Initialise(configuration.Database);

            ServerManager.Instance.Initialise();

            MessageManager.Instance.Initialise();
            NetworkManager<Session>.Instance.Initialise(configuration.Network);

            WorldManager.Instance.Initialise(NetworkManager<Session>.Instance.Update);

            log.Info("Ready!");

            while (true)
            {
                Console.Write(">> ");
                string command = Console.ReadLine();
                CommandManager.Instance.Invoke(command);
            }
        }
    }
}
