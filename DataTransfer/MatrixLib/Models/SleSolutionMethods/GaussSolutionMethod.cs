namespace MatrixLib.Models.SleSolutionMethods
{
    /// <summary>
    /// Represents gauss solution method
    /// </summary>
    public class GaussSolutionMethod : ISleSolutionMethod
    {
        public event Action OnSolved;

        protected SLE _sle;

        /// <summary>
        /// Gauss solution method constructor
        /// </summary>
        /// <param name="sle">Sle to solve by gauss linear method</param>
        public GaussSolutionMethod(SLE sle)
        {
            _sle = sle;
        }

        /// <summary>
        /// Solve sle method
        /// </summary>
        /// <returns>Matrix with soltions of sle</returns>
        public virtual Matrix<double> SolveSle()
        {
            StraightRun();
            return ReverseRun();
        }

        /// <summary>
        /// Matrix straight run
        /// </summary>
        protected virtual void StraightRun()
        {
            for (int i = 0; i < _sle.MatrixCoefficients.GetHeight; i++)
            {
                Calculate(i);
            }
        }

        /// <summary>
        /// Method alculates matrix coefficients
        /// </summary>
        /// <param name="i">Line depends on which calculate</param>
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

        /// <summary>
        /// Reverse run method
        /// </summary>
        /// <returns>Matrix with sle solutions</returns>
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
