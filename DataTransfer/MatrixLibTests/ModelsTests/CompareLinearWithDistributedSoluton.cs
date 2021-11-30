using MatrixLib.Models;
using MatrixLib.Models.SleSolutionMethods;
using ProfilerLib.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MatrixLibTests.ModelsTests
{
    public class CompareLinearWithDistributedSoluton
    {
        [Fact]
        public void CompareLinearWithDistributedSolution()
        {
            Matrix<double> expected = new Matrix<double>(new double[,]
           {
              { -3, -1, 2, 7 }
           });

            Matrix<double> coefficients = new Matrix<double>(new double[,]
            {
                {3,2,1,1},
                {1,-1,4,-1 },
                {-2,-2,-3,1 },
                {1,5,-1,2 },
            });

            Matrix<double> freeMembers = new Matrix<double>(new double[,]
            {
                {-2 },
                {-1 },
                {9 },
                {4 },
            });

            SLE sle = new SLE(coefficients, freeMembers);

            IProfiler profiler = new Profiler("Distributed", "L",
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle), () => new GaussSolutionMethod(sle)),
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle), () => new GaussSolutionMethod(sle)),
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle), () => new GaussSolutionMethod(sle)));
            var profileData = profiler.Profile();
        }
    }
}
