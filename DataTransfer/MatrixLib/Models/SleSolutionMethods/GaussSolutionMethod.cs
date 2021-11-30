namespace MatrixLib.Models.SleSolutionMethods
{
    public class GaussSolutionMethod : ISleSolutionMethod
    {
        public event Action OnSolved;

        protected SLE _sle;

        public GaussSolutionMethod(SLE sle)
        {
            _sle = sle;
        }

        public virtual Matrix<double> SolveSle()
        {
            StraightRun();

            return ReverseRun();
        }

        protected virtual void StraightRun()
        {
            for (int i = 0; i < _sle.MatrixCoefficients.GetHeight; i++)
            {
                Calculate(i);
            }
        }

        protected virtual void Calculate(int i)
        {
            var coefficients = _sle.MatrixCoefficients;
            for (int j = i + 1; j < coefficients.GetWidth; j++)
            {
                double d = coefficients[j, i] / coefficients[i, i];
                for (int k = i; k < coefficients.GetWidth; k++)
                {
                    coefficients[j, k] = coefficients[j, k] - d * coefficients[i, k];
                }

                _sle.FreeMembers[j, 0] -= d * _sle.FreeMembers[i, 0];
            }
        }

        protected Matrix<double> ReverseRun()
        {
            var coefficients = _sle.MatrixCoefficients;
            Matrix<double> x = new Matrix<double>(1, coefficients.GetHeight);

            for (int i = coefficients.GetHeight - 1; i >= 0; i--)
            {
                double d = 0;
                for (int j = i; j < coefficients.GetWidth ; j++)
                {

                    double s = coefficients[i, j] * x[0, j];
                    d += s;
                }

                _sle.FreeMembers[i, 0] = Math.Round((_sle.FreeMembers[i, 0] - d) / coefficients[i, i], 2);
                x[0, i] = _sle.FreeMembers[i, 0];
            }

            return x;
        }
    }
}
