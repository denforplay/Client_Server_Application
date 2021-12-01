using MatrixLib.Core;

namespace MatrixLib.Models.SleSolutionMethods
{
    public class DistributedGaussSolution : GaussSolutionMethod
    {
        public DistributedGaussSolution(SLE sle) : base(sle)
        {
        }

        protected override void Calculate(int i)
        {
            var coefficients = _sle.MatrixCoefficients;
            Parallel.For(i + 1, coefficients.GetHeight, (j) =>
            {
                double d = coefficients[j, i] / coefficients[i, i];
                Parallel.For(i, coefficients.GetWidth, (k) =>
                {
                    coefficients[j, k] = coefficients[j, k] - d * coefficients[i, k];
                });


                _sle.FreeMembers[j, 0] -= d * _sle.FreeMembers[i, 0];
            });

                
        }
    }
}
