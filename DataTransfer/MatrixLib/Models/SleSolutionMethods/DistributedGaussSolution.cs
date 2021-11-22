using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixLib.Models.SleSolutionMethods
{
    public class DistributedGaussSolution : GaussSolutionMethod
    {
        public DistributedGaussSolution(Matrix<double> sleMatrix) : base(sleMatrix)
        {
        }

        public override double[] SolveSle()
        {
            double[] x = new double[_sleMatrix.GetHeight];
            List<EventThread> threads = new List<EventThread>();

            for (int i = 0; i < _sleMatrix.GetHeight - 1; i++)
            {
                int j = i;
                EventThread thread = new EventThread(() => Calculate(j));
                threads.Add(thread);
            }

            for (int i = 0; i < threads.Count - 1; i++)
            {
                int j = i;
                threads[j].OnThreadCompleted += () => threads[j + 1].Start();
            }

            threads.First().Start();

            threads.Last().OnThreadCompleted += () =>
            {
                x = ReverseRun();
            };

            return x;
        }
    }
}
