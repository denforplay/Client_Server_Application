using MatrixLib.Models;

namespace MatrixLib.Controllers
{
    /// <summary>
    /// Represents controller for sle
    /// </summary>
    public class SleController
    {
        private SLE _sle;
        
        /// <summary>
        /// Sle controller
        /// </summary>
        /// <param name="sle">System of linear equations</param>
        public SleController(SLE sle)
        {
            _sle = sle;
        }

        /// <summary>
        /// Generate sle of size with random elements
        /// </summary>
        /// <param name="size">Size of sle</param>
        public void GenerateSle(int size)
        {
            Random random = new Random();
            _sle.MatrixCoefficients.ChangeMatrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _sle.MatrixCoefficients[i, j] = random.Next(1, 10);
                }
            }

            _sle.FreeMembers.ChangeMatrix(size, 1);
            for (int i = 0; i < _sle.FreeMembers.GetHeight; i++)
            {
                _sle.FreeMembers[i, 0] = random.Next(1, 10);
            }
        }
    }
}
