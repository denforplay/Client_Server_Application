using MatrixLib.Core;

namespace MatrixLib.Models.SleSolutionMethods
{
    /// <summary>
    /// Represents distributed gauss solution
    /// </summary>
    public class DistributedGaussSolution : GaussSolutionMethod
    {
        private List<EventTask> _tasks;
        
        /// <summary>
        /// Distributed gauss solution constructor
        /// </summary>
        /// <param name="sle">Sle to solve</param>
        /// <param name="threadsCount">Count of threads</param>
        public DistributedGaussSolution(SLE sle, int threadsCount) : base(sle)
        {
            _tasks = new List<EventTask>();
            for (int i = 0; i < threadsCount; i++)
            {
                _tasks.Add(new EventTask(default));
            }

            if (sle.MatrixCoefficients.GetHeight % threadsCount != 0)
            {
                _tasks.Add(new EventTask(default));
            }
        }

        protected override void StraightRun()
        {
            List<EventTask> threads = new List<EventTask>();
            int actionsPerThread = (int)Math.Ceiling((decimal)_sle.MatrixCoefficients.GetHeight / _tasks.Count);
            int currentTask = 0;
            for (int i = 0; i < _sle.MatrixCoefficients.GetHeight; i++)
            {
                int actionsCount = actionsPerThread;
                Action calculatingActions = default;
                while (actionsCount-- > 0)
                {
                    int j = i;
                    calculatingActions += () => Calculate(j);
                    i++;
                }

                _tasks[currentTask] = new EventTask(calculatingActions);
                _tasks[currentTask++].Start().Wait();
                i--;
            }
        }

        protected override void Calculate(int i)
        {
            var coefficients = _sle.MatrixCoefficients;
            Parallel.For(i + 1, coefficients.GetHeight, (j) =>
            {
                double d = coefficients[j, i] / coefficients[i, i];
                for (int k = i; k < coefficients.GetWidth; k++)
                {
                    coefficients[j, k] = coefficients[j, k] - d * coefficients[i, k];

                }

                _sle.FreeMembers[j, 0] -= d * _sle.FreeMembers[i, 0];
            });
        }
    }
}
