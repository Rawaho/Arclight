using System;
using System.IO;
using System.Reflection;
using Arclight.Server.Character.Network;
using Arclight.Shared.Command;
using Arclight.Shared.Configuration;
using Arclight.Shared.Database;
using Arclight.Shared.Game;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;
using NLog;

namespace Arclight.Server.Character
{
    internal static class CharacterServer
    {
        #if DEBUG
        private const string Title = "Arclight Character Server (DEBUG)";
        #else
        private const string Title = "Arclight Character Server (RELEASE)";
        #endif

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            Console.Title = Title;
            log.Info("Initialising...");

            CommandManager.Instance.Initialise();

            ConfigurationManager<CharacterServerConfig>.Instance.Initialise("CharacterServer.json");
            CharacterServerConfig configuration = ConfigurationManager<CharacterServerConfig>.Instance.Model;

            DatabaseManager.Instance.Initialise(configuration.Database);

            ServerManager.Instance.Initialise();

            MessageManager.Instance.Initialise();
            NetworkManager<CharacterSession>.Instance.Initialise(configuration.Network);

            WorldManager.Instance.Initialise(NetworkManager<CharacterSession>.Instance.Update);

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
