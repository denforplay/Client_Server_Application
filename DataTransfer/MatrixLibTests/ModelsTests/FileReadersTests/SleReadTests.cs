using MatrixLib.Models;
using MatrixLib.Models.FileReader;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace MatrixLibTests.ModelsTests.FileReadersTests
{
    public class SleReadTests
    {
        Matrix<double> coefficients = new Matrix<double>(new double[,]
         {
                {1, 2 },
                {3, -1 }
         });

        Matrix<double> freeMembers = new Matrix<double>(new double[,]
        {
               {11 },
               {12 }
        });


        [Fact]
        public void TestReadSle_NullRegexReader_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SleReader(null, @$"regextest2.txt"));
        }

        [Fact]
        public void TestReadSle_NullFilepathString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SleReader(new RegexReader(), null));
        }

        [Fact]
        public void TestReadSle()
        {

            var expectedSle = new SLE(coefficients, freeMembers);
            IFileReader<SLE> fileReader = new SleReader(new Regex(@"\-?\d+(\,\d+)?"));
            var actualSle = fileReader.ReadData(@$"test.txt");
            Assert.Equal(expectedSle, actualSle);
        }

        [Fact]
        public void TestReadSleUsingRegexReader()
        {
            var expectedSle = new SLE(coefficients, freeMembers);
            IFileReader<SLE> sleReader = new SleReader(new RegexReader(), "regextest2.txt");
            var actualSle = sleReader.ReadData(@"test.txt");
            Assert.Equal(expectedSle, actualSle);
        }
    }
}
