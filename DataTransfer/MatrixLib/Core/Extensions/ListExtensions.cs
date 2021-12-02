using MatrixLib.Models;

namespace MatrixLib.Core.Extensions
{
    /// <summary>
    /// Provides extensions to list collection
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Convert list to matrix
        /// </summary>
        /// <typeparam name="T">Typeof list data</typeparam>
        /// <param name="list">List to convert into matrix</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if list is empty</exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static Matrix<T> ToMatrix<T>(this List<List<T>> list) where T : IComparable
        {
            if (list.Count == 0 || list[0].Count == 0)
                throw new ArgumentException("The list must have non-zero dimensions.");

            var result = new Matrix<T>(list.Count, list[0].Count);
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i].Count != list[0].Count)
                        throw new InvalidOperationException("The list cannot contain elements (lists) of different sizes.");
                    result[i, j] = list[i][j];
                }
            }

            return result;
        }
    }
}
