using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatrixLib.Models.FileReader
{
    /// <summary>
    /// Represents class for reading regular expression from file
    /// </summary>
    public class RegexReader : IFileReader<Regex>
    {
        /// <summary>
        /// Read data from file by filepath
        /// </summary>
        /// <param name="filepath">Path to file from where read data</param>
        /// <returns>Regex readed from file</returns>
        /// <exception cref="FileNotFoundException">Throws if on there is not file on filepath</exception>
        public Regex ReadData(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException("File is not exists", nameof(filepath));

            string patternFromFile = File.ReadAllLines(filepath).First();
            return new Regex(patternFromFile);
        }
    }
}
