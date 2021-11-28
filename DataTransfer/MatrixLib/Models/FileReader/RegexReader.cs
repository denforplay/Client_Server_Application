﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatrixLib.Models.FileReader
{
    public class RegexReader : IFileReader<Regex>
    {
        public Regex ReadData(string filepath)
        {
            string patternFromFile = File.ReadAllLines(filepath).First();
            return new Regex(patternFromFile);
        }
    }
}
