using System;
using System.Threading.Tasks;

namespace Arclight.Shared.Event
{
    public class TaskEvent : IEvent
    {
        private readonly Task task;
        private readonly Action callback;

        public TaskEvent(Task task, Action callback)
        {
            this.task     = task;
            this.callback = callback;
        }

        public bool CanExecute()
        {
            return task.IsCompleted;
        }

        public void Execute()
        {
            callback.Invoke();
        }
    }

    public class TaskEvent<T> : IEvent
    {
        private readonly Task<T> task;
        private readonly Action<T> callback;

        public TaskEvent(Task<T> task, Action<T> callback)
        {
            this.task     = task;
            this.callback = callback;
        }

        public bool CanExecute()
        {
            return task.IsCompleted;
        }

        public void Execute()
        {
            callback.Invoke(task.Result);
        }
    }
}
