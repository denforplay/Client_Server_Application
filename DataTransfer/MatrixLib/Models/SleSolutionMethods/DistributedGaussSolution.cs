using MatrixLib.Core;

namespace MatrixLib.Models.SleSolutionMethods
{
    public class DistributedGaussSolution : GaussSolutionMethod
    {
        public DistributedGaussSolution(SLE sle) : base(sle)
        {
        }

        public override Matrix<double> SolveSle()
        {
            var coefficients = _sle.MatrixCoefficients;
            Matrix<double> x = new Matrix<double>(1, coefficients.GetHeight);
            StraightRun();
            return ReverseRun();
        }

        protected override void StraightRun()
        {
            for (int i = 0; i < _sle.MatrixCoefficients.GetHeight; i++)
            {
                Calculate(i);
            }
        }

        protected override void Calculate(int i)
        {
            var coefficients = _sle.MatrixCoefficients;

            for (int j = i + 1; j < coefficients.GetWidth; j++)
            {
                double d = coefficients[j, i] / coefficients[i, i];
                Parallel.For(i, coefficients.GetWidth, (k) =>
                {
                    coefficients[j, k] = coefficients[j, k] - d * coefficients[i, k];
                });
               

                _sle.FreeMembers[j, 0] -= d * _sle.FreeMembers[i, 0];
            }
        }
    }
}
