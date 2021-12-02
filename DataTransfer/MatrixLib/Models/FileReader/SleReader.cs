using MatrixLib.Core.Extensions;
using System.Text.RegularExpressions;

namespace MatrixLib.Models.FileReader
{
    public class SleReader : IFileReader<SLE>
    {
        private Regex _regex;

        public SleReader(IFileReader<Regex> fileReader, string regexFilePath)
        {
            if (fileReader is null)
                throw new ArgumentNullException("Empty file reader", nameof(fileReader));

            _regex = fileReader.ReadData(regexFilePath);
        }

        public SleReader(Regex regex)
        {
            if (regex is null)
                throw new ArgumentNullException("Regex can't be empty", nameof(regex));

            _regex = regex;
        }

        public SLE ReadData(string filepath)
        {
            string[] sleData = File.ReadAllLines(filepath);
            return Read(sleData);
        }

        public SLE Read(string[] sleData)
        {
            List<List<double>> coeficients = new List<List<double>>();
            List<List<double>> freeMembers = new List<List<double>>();
            for (int i = 0; i < sleData.Length; i++)
            {
                List<double> numbers = new List<double>();
                var finded = _regex.Matches(sleData[i]).Cast<Match>().Select(m => m.Value).ToArray();
                for (int j = 0; j < finded.Length - 1; j++)
                {
                    double parsed = float.Parse(finded[j]);
                    numbers.Add(parsed);
                }

                coeficients.Add(numbers);
                freeMembers.Add(new List<double> { float.Parse(finded.Last()) });
            }

            SLE sle = new SLE(coeficients.ToMatrix(), freeMembers.ToMatrix());
            return sle;
        }
    }
}
