using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Arclight.Shared.GameTable.Model;
using NLog;

namespace Arclight.Shared.GameTable
{
    public class GameTableManager : Singleton<GameTableManager>
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        [GameTable("tb_district.res")]
        public GameTable<DistrictEntry> District { get; private set; }

        private GameTableManager()
        {
        }

        public void Initialise(string path)
        {
            log.Info("Loading tables...");

            if (!Directory.Exists(path))
                throw new GameTableException($"Table path {path} is invalid!");

            foreach (PropertyInfo info in GetType().GetProperties())
            {
                GameTableAttribute attribute = info.GetCustomAttribute<GameTableAttribute>();
                if (attribute == null)
                    continue;

                string filePath = $"{path}/{attribute.Name}";
                if (!File.Exists(filePath))
                    throw new GameTableException($"Table file {filePath} is invalid!");

                Stopwatch sw = Stopwatch.StartNew();
                info.SetValue(this, Activator.CreateInstance(info.PropertyType, filePath));

                log.Info($"Loaded table {attribute.Name} in {sw.ElapsedMilliseconds}ms.");
            }
        }
    }
}
