namespace MatrixLib.Core
{
    /// <summary>
    /// Represents task with event
    /// </summary>
    public class EventTask
    {
        public event Action OnThreadCompleted;
        private Task _task;

        /// <summary>
        /// Event task constructor
        /// </summary>
        /// <param name="method">Method to complete in task</param>
        public EventTask(Action method)
        {
            _task = new Task(() =>
            {
                method();
                OnThreadCompleted?.Invoke();
            });
        }

        /// <summary>
        /// Start complete task
        /// </summary>
        /// <returns>Task which start to work</returns>
        public Task Start()
        {
            _task.Start();
            return _task;
        }
    }
}
