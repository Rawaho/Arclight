using System;
using System.Collections.Generic;
using NLog;

namespace Arclight.Shared.Event
{
    public class EventManager : IUpdate
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly Queue<IEvent> events = new Queue<IEvent>();

        public void Update(double tick)
        {
            while (events.TryPeek(out IEvent @event))
            {
                if (!@event.CanExecute())
                    return;

                events.Dequeue();

                try
                {
                    @event.Execute();
                }
                catch (Exception exception)
                {
                    log.Error(exception);
                }
            }
        }

        /// <summary>
        /// Enqueue <see cref="IEvent"/> to be delay executed.
        /// </summary>
        public void Enqueue(IEvent @event) => events.Enqueue(@event);
    }
}
