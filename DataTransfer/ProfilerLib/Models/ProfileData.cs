namespace ProfilerLib.Models
{
    public class ProfileData : Dictionary<string, List<double>>
    {
        public void AddData(string key, double value)
        {
            if (TryGetValue(key, out var values))
            {
                values.Add(value);
            }
            else
            {
                Add(key, new List<double> { value });
            }

        }
    }
}
