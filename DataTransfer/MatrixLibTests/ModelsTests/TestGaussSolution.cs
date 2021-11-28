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
            Matrix<double> expected = new Matrix<double>(new double[,]
            {
              { 5,3 }
            });

            Matrix<double> coefficients = new Matrix<double>(new double[,]
            {
                {1, 2 },
                {3, -1 }
            });

            Matrix<double> freeMembers = new Matrix<double>(new double[,]
            {
               {11 },
               {12 }
            });

            SLE sle = new SLE(coefficients, freeMembers);
            ISleSolutionMethod solutionMethod = new GaussSolutionMethod(sle);
            var x = solutionMethod.SolveSle();
            Assert.True(expected.Equals(x));
        }

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

            ISleSolutionMethod solutionMethod = new GaussSolutionMethod(sle);
            var x = solutionMethod.SolveSle();
            Assert.True(expected.Equals(x));
        }
    }
}
