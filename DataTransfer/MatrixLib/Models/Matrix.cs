using System.Text;

namespace MatrixLib.Models
{
    /// <summary>
    /// Represents matrix
    /// </summary>
    /// <typeparam name="T">Type of matrix data</typeparam>
    public class Matrix<T> where T : IComparable
    {
        private T[,] _matrix;

        /// <summary>
        /// Matrix contructor
        /// </summary>
        /// <param name="matrix">Double dimension array</param>
        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        /// <summary>
        /// Matrix constructor
        /// </summary>
        /// <param name="height">Matrix height</param>
        /// <param name="width">Matrix width</param>
        public Matrix(int height, int width)
        {
            _matrix = new T[height, width];
        }

        /// <summary>
        /// Return matrix height
        /// </summary>
        public int GetHeight => _matrix.GetLength(0);

        /// <summary>
        /// Return matrix width
        /// </summary>
        public int GetWidth => _matrix.GetLength(1);

        /// <summary>
        /// Method for changing matrix size
        /// </summary>
        /// <param name="height">New matrix height</param>
        /// <param name="width">New matrix width</param>
        public void ChangeMatrix(int height, int width)
        {
            _matrix = new T[height, width];
        }

        public T this[int i, int j]
        {
            get
            {
                return _matrix[i, j];
            }

            set
            {
                _matrix[i, j] = value;
            }
        }

        /// <summary>
        /// Method to create a copy of current matrix
        /// </summary>
        /// <returns>Copy of a current matrix</returns>
        public Matrix<T> Clone()
        {
            Matrix<T> clone = new Matrix<T>(GetHeight, GetWidth);
            for (int i = 0; i < GetHeight; i++)
            {
                for (int j = 0; j < GetWidth; j++)
                {
                    clone[i, j] = this[i, j];
                }
            }

            return clone;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Matrix<T> otherMatrix)
            {
                if (otherMatrix.GetHeight != GetHeight || otherMatrix.GetWidth != GetWidth)
                {
                    return false;
                }
                else
                {
                    for(int i = 0; i < GetHeight; i++)
                    {
                        for (int j = 0; j < GetWidth; j++)
                        {
                            if (_matrix[i, j].CompareTo(otherMatrix[i, j]) != 0)
                                return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                str.Append($"{i + 1} row: ");
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    str.Append($"{_matrix[i, j]} ");
                }

                str.Append(Environment.NewLine);
            }

            return str.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 1111;

            for (int i = 0; i < GetHeight; i++)
            {
                for (int j = 0; j < GetWidth; j++)
                {
                    hash += _matrix[i, j].GetHashCode();
                }
            }

            return hash;
        }
    }
}
