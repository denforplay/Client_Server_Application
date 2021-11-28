using System.Text.RegularExpressions;

namespace MatrixLib.Models
{
    public sealed class SLE
    {
        private readonly Matrix<double> _matrixCoefficients;
        private readonly Matrix<double> _freeMembers;

        public Matrix<double> MatrixCoefficients => _matrixCoefficients;
        public Matrix<double> FreeMembers => _freeMembers;

        public SLE(Matrix<double> сoefficientsMatrix, Matrix<double> freeMembers)
        {
            _matrixCoefficients = сoefficientsMatrix;
            _freeMembers = freeMembers;
        }

        public SLE(string text, Regex lineRegex)
        {
        }
    }
}
