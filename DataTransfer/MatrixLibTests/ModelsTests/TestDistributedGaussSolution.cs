using MatrixLib.Models;
using MatrixLib.Models.SleSolutionMethods;
using System.Net.Http;
using Xunit;

namespace MatrixLibTests.ModelsTests
{
    public sealed class TestDistributedGaussSolution
    {
        [Fact]
        public void Test4x4Sle()
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
            DistributedGaussSolution solutionMethod = new DistributedGaussSolution(sle, 2);
            var x = solutionMethod.SolveSle();
            Assert.True(x.Equals(expected));
        }

        [Fact]
        public void Test3x3Sle()
        {
            Matrix<double> expected = new Matrix<double>(new double[,]
            {
              { 1, 3, 2 }
            });

            Matrix<double> coefficients = new Matrix<double>(new double[,]
            {
                {3,-2,5},
                {7,4,-8},
                {5,-3,-4},
            });

            Matrix<double> freeMembers = new Matrix<double>(new double[,]
            {
                {7 },
                {3 },
                {-12 },
            });

            SLE sle = new SLE(coefficients, freeMembers);
            DistributedGaussSolution solutionMethod = new DistributedGaussSolution(sle, 2);
            var x = solutionMethod.SolveSle();
            Assert.True(x.Equals(expected));
        }
    }
}
