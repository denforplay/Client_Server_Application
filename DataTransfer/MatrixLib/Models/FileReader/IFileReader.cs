namespace MatrixLib.Models.FileReader
{
    /// <summary>
    /// Provides functionality to read data from file
    /// </summary>
    /// <typeparam name="T">Type of readed data</typeparam>
    public interface IFileReader<out T>
    {
        public T ReadData(string filepath);
    }
}
