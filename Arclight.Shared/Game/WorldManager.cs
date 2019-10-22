using System;
using System.Diagnostics;
using System.Threading;

namespace Arclight.Shared.Game
{
    public sealed class WorldManager : Singleton<WorldManager>
    {
        private volatile bool shutdownRequested;

        private WorldManager()
        {
        }

        public void Initialise(Action<double> updateAction)
        {
            var worldThread = new Thread(() =>
            {
                var sw = new Stopwatch();
                double lastTick = 0d;

                while (!shutdownRequested)
                {
                    sw.Restart();

                    updateAction(lastTick);

                    Thread.Sleep(1);
                    lastTick = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                }
            });

            worldThread.Start();
        }

        /// <summary>
        /// Request shutdown of world thread.
        /// </summary>
        public void Shutdown()
        {
            shutdownRequested = true;
        }
    }
}
