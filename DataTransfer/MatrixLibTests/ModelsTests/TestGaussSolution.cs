using MatrixLib.Models;
using MatrixLib.Models.SleSolutionMethods;
using Xunit;

namespace MatrixLibTests.ModelsTests
{
    public sealed class TestGaussSolution
    {
        [Fact]
        public void Test2x2Sle()
        {
            double[] expected = new double[]
            {
                5, 3
            };

            Matrix<double> sle = new Matrix<double>(new double[,]
            {
                { 1, 2, 11},
                {3, -1, 12 }
            });

            ISleSolutionMethod solutionMethod = new GaussSolutionMethod(sle);
            var x = solutionMethod.SolveSle();
            Assert.Equal(expected, x);
        }

        [Fact]
        public void Test4x4Sle()
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
                {1,5,-1,2,4 }
            });

            ISleSolutionMethod solutionMethod = new GaussSolutionMethod(sle);
            var x = solutionMethod.SolveSle();
            Assert.Equal(expected, x);
        }
    }
}
