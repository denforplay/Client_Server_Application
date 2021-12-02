using MatrixLib.Models.FileReader;
using System.Text.RegularExpressions;

namespace MatrixLib.Models.Parsers.MatrixParsers
{
    public class SleFromUrl : SleParserBase
    {
        public override SLE ParseFrom(string from)
        {
            SleReader reader = new SleReader(new Regex(@"\-?\d{1,}"));
            var splittedRequestData = Regex.Split(from, @"(\w{1,3}.=\-?\d{1,})");
            List<string> sleData = new List<string>();
            for (int i = 0; i < splittedRequestData.Length - 1; i += 2)
            {
                string sleString = splittedRequestData[i] + splittedRequestData[i + 1];
                sleData.Add(sleString.Trim('&').Replace('&', ' ').Replace(".", string.Empty));
            }

            return reader.Read(sleData.ToArray());
        }
    }
}
