namespace MatrixLib.Models.SleSolutionMethods
{
    /// <summary>
    /// Provides functionality to solve sle
    /// </summary>
    public interface ISleSolutionMethod
    {
        /// <summary>
        /// Method to solve sle
        /// </summary>
        /// <returns>Matrix with solutions</returns>
        Matrix<double> SolveSle();
    }
}
