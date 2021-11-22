using MatrixLib.Models;
using MatrixLib.Models.SleSolutionMethods;
using Xunit;

namespace MatrixLibTests.ModelsTests
{
    public sealed class TestDistributedGaussSolution
    {
        [Fact]
        public void Test()
        {
            double[] expected = new double[]
           {
                -3, -1, 2, 7
           };

            Matrix<double> sle = new Matrix<double>(new double[,]
            {
                {3,2,1,1,-2 },
                {1,-1,4,-1,-1 },
                {-2,-2,-3,1,9 },
                {1,5,-1,2,4 },
            });

            DistributedGaussSolution solutionMethod = new DistributedGaussSolution(sle);
            var x = solutionMethod.SolveSle();
            solutionMethod.OnSolved += () =>
            Assert.Equal(expected, x);
        }
    }
}
