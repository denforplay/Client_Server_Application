using System.Text.RegularExpressions;

namespace MatrixLib.Models
{
    /// <summary>
    /// Class represents system of linear equations
    /// </summary>
    public sealed class SLE
    {
        private readonly Matrix<double> _matrixCoefficients;
        private readonly Matrix<double> _freeMembers;

        /// <summary>
        /// Matrix coefficients
        /// </summary>
        public Matrix<double> MatrixCoefficients => _matrixCoefficients;

        /// <summary>
        /// Matrix free members
        /// </summary>
        public Matrix<double> FreeMembers => _freeMembers;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SLE()
        {
            _matrixCoefficients = new Matrix<double>(0, 0);
            _freeMembers = new Matrix<double>(0, 0);
        }

        /// <summary>
        /// Matrix constructor
        /// </summary>
        /// <param name="сoefficientsMatrix"></param>
        /// <param name="freeMembers"></param>
        public SLE(Matrix<double> сoefficientsMatrix, Matrix<double> freeMembers)
        {
            _matrixCoefficients = сoefficientsMatrix;
            _freeMembers = freeMembers;
        }
    }
}
