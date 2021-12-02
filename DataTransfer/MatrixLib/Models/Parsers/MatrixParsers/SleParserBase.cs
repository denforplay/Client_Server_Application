namespace MatrixLib.Models.Parsers.MatrixParsers
{
    public abstract class SleParserBase : IParser<SLE>
    {
        public abstract SLE ParseFrom(string from);
    }
}
