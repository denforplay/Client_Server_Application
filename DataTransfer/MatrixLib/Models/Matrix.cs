using System.Text;

namespace MatrixLib.Models
{
    public class Matrix<T>
    {
        private T[,] _matrix;

        public int GetHeight => _matrix.GetLength(0);
        public int GetWidth => _matrix.GetLength(1);

        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(int height, int width)
        {
            _matrix = new T[height, width];
        }

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
    }
}
