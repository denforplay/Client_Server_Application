namespace MatrixLib.Models.Parsers.MatrixParsers
{
    /// <summary>
    /// Represents base class to parse sle from string
    /// </summary>
    public abstract class SleParserBase : IParser<SLE>
    {
        /// <summary>
        /// Method to parse sle from string
        /// </summary>
        /// <param name="from">String to parse from</param>
        /// <returns>Sle, readed from string</returns>
        public abstract SLE ParseFrom(string from);
    }
}
