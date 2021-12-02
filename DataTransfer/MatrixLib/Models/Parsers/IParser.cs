namespace MatrixLib.Models.Parsers
{
    /// <summary>
    /// Describes base functionality of parser
    /// </summary>
    /// <typeparam name="T">Type of object to parse from string</typeparam>
    public interface IParser<out T>
    {
        /// <summary>
        /// Method to parse from string
        /// </summary>
        /// <param name="from">String from what parse object</param>
        /// <returns>New object of type T</returns>
        T ParseFrom(string from);
    }
}
