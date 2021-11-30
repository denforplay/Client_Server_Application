using System.Diagnostics;

namespace ProfilerLib.Core.Extensions
{
    public static class StopWatchExtensions
    {
        public static TimeSpan GetElapsedTime(this Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
