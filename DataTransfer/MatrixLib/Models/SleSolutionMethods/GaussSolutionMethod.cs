namespace MatrixLib.Models.SleSolutionMethods
{
    public class GaussSolutionMethod : ISleSolutionMethod
    {
        public event Action OnSolved;

        protected Matrix<double> _sleMatrix;

        public GaussSolutionMethod(Matrix<double> sleMatrix)
        {
            _sleMatrix = sleMatrix;
        }

        public virtual double[] SolveSle()
        {
            StraightRun();

            return ReverseRun();
        }

        protected void StraightRun()
        {
            for (int i = 0; i < _sleMatrix.GetHeight - 1; i++)
            {
                Calculate(i);
            }
        }

        protected void Calculate(int i)
        {
            for (int j = i + 1; j < _sleMatrix.GetWidth - 1; j++)
            {
                double d = _sleMatrix[j, i] / _sleMatrix[i, i];
                for (int k = i; k < _sleMatrix.GetWidth - 1; k++)
                {
                    _sleMatrix[j, k] = _sleMatrix[j, k] - d * _sleMatrix[i, k];
                }

                _sleMatrix[j, _sleMatrix.GetWidth - 1] -= d * _sleMatrix[i, _sleMatrix.GetWidth - 1];
            }
        }

        protected double[] ReverseRun()
        {
            double[] x = new double[_sleMatrix.GetHeight];

            for (int i = _sleMatrix.GetHeight - 1; i >= 0; i--)
            {
                double d = 0;
                for (int j = i; j < _sleMatrix.GetWidth - 1; j++)
                {

                    double s = _sleMatrix[i, j] * x[j];
                    d += s;
                }

                x[i] = Math.Round((_sleMatrix[i, _sleMatrix.GetWidth - 1] - d) / _sleMatrix[i, i], 2);
            }

            return x;
        }
    }
}
