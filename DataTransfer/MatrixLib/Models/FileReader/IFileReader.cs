namespace MatrixLib.Models.FileReader
{
    public interface IFileReader<out T>
    {
        public T ReadData(string filepath);
    }
}
