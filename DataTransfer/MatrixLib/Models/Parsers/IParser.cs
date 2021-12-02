namespace MatrixLib.Models.Parsers
{
    public interface IParser<out T>
    {
        T ParseFrom(string from);
    }
}
