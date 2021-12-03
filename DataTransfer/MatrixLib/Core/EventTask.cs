using System;
using System.Threading;

namespace MatrixLib.Core
{
    public class EventTask
    {
        public event Action OnThreadCompleted;
        private Task _task;

        public EventTask(Action method)
        {
            _task = new Task(() =>
            {
                method();
                OnThreadCompleted?.Invoke();
            });
        }

        public Task Start()
        {
            _task.Start();
            return _task;
        }
    }
}
