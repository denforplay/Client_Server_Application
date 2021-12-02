using MatrixLib.Controllers;
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
        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void CompareLinearWithDistributedSolution(int size)
        {
            SLE sle = new SLE();
            SleController sleController = new SleController(sle);
            sleController.GenerateSle(size);
            IProfiler profiler = new Profiler("Distributed", "Linear",
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle).SolveSle(), () => new GaussSolutionMethod(sle).SolveSle()),
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle).SolveSle(), () => new GaussSolutionMethod(sle).SolveSle()),
                new KeyValuePair<Action, Action>(() => new DistributedGaussSolution(sle).SolveSle(), () => new GaussSolutionMethod(sle).SolveSle()));
            var profileData = profiler.Profile();
        }
    }
}
