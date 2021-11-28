using System;
using System.Threading;

namespace MatrixLib.Core
{
    public class EventThread
    {
        public event Action OnThreadCompleted;
        private Thread _thread;

        public EventThread(Action method)
        {
            _thread = new Thread(new ThreadStart(() =>
            {
                method.Invoke();
                OnThreadCompleted?.Invoke();
            }));
        }

        public void Start()
        {
            _thread.Start();
        }
    }
}
