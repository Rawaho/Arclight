using System;
using System.IO;
using System.Reflection;
using Arclight.Shared.Configuration;
using Arclight.Shared.Game;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;
using Arclight.Server.World.Network;
using Arclight.Shared.Command;
using Arclight.Shared.Database;
using NLog;

namespace Arclight.Server.World
{
    internal static class WorldServer
    {
        #if DEBUG
        private const string Title = "Arclight World Server (DEBUG)";
        #else
        private const string Title = "Arclight World Server (RELEASE)";
        #endif

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            Console.Title = Title;
            log.Info("Initialising...");

            CommandManager.Instance.Initialise();

            ConfigurationManager<WorldServerConfig>.Instance.Initialise("WorldServer.json");
            WorldServerConfig configuration = ConfigurationManager<WorldServerConfig>.Instance.Model;

            DatabaseManager.Instance.Initialise(configuration.Database);
            DatabaseManager.Instance.Migrate();

            MessageManager.Instance.Initialise();
            NetworkManager<WorldSession>.Instance.Initialise(configuration.Network);

            WorldManager.Instance.Initialise(lastTick =>
            {
                NetworkManager<WorldSession>.Instance.Update(lastTick);
            });

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
