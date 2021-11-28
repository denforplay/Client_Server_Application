﻿using MatrixLib.Models;
using MatrixLib.Models.FileReader;
using System.Text.RegularExpressions;
using Xunit;

namespace MatrixLibTests.ModelsTests.FileReadersTests
{
    public class SleReadTests
    {
        [Fact]
        public void TestReadSle()
        {
            IFileReader<SLE> fileReader = new SleReader(new Regex(@"\-?\d+(\,\d+)?"));
            var sle = fileReader.ReadData(@"C:\AAAA\test.txt");
        }
    }
}
