namespace MatrixLib.Models.FileReader
{
    public interface IFileReader<T>
    {
        public T ReadData(string filepath);
    }
}
