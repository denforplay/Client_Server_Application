using MatrixLib.Models.FileReader;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace MatrixLibTests.ModelsTests.FileReadersTests
{
    public class RegexReadTests
    {
        IFileReader<Regex> regexReader = new RegexReader();

        [Fact]
        public void TestReadRegex()
        {
            Regex expectedRegex = new Regex(@"\d+");
            Regex actualRegex = regexReader.ReadData(@"regextest.txt");
            Assert.Equal(expectedRegex.ToString(), actualRegex.ToString());
        }

        [Fact]
        public void TestReadRegexFromNotExistFile()
        {
            Assert.Throws<FileNotFoundException>(() => regexReader.ReadData("eee.txt"));
        }
    }
}
