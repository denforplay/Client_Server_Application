using MatrixLib.Core;
using ProfilerLib.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilerLib.Models
{
    public class Profiler : IProfiler
    {
        private KeyValuePair<Action, Action>[] _actions;
        private string _firstKey;
        private string _secondKey;

        public Profiler(string firstKey, string secondKey, params KeyValuePair<Action, Action>[] actions)
        {
            _actions = actions;
            _firstKey = firstKey;
            _secondKey = secondKey;
        }

        public ProfileData Profile()
        {
            ProfileData profileData = new ProfileData();

            foreach(var actionsPair in _actions)
            {
                double firstTime = actionsPair.Value.GetElapsedTime().TotalMilliseconds;
                double secondTime = actionsPair.Key.GetElapsedTime().TotalMilliseconds;
                if (profileData.TryGetValue(_firstKey, out List<double> results1))
                {
                    results1.Add(firstTime);
                }
                else
                {
                    profileData.Add(_firstKey, new List<double> { firstTime });
                }

                if (profileData.TryGetValue(_secondKey, out List<double> results2))
                {
                    results2.Add(secondTime);
                }
                else
                {
                    profileData.Add(_secondKey, new List<double> { secondTime });
                }
            }

            return profileData;
        }

        private double CalculateTime(Action action)
        {
            Stopwatch sw = new Stopwatch();
            EventThread th = new EventThread(action);
            th.OnThreadCompleted += () => sw.Stop();
            sw = Stopwatch.StartNew();
            th.Start();
            return sw.Elapsed.TotalMilliseconds;
        }
    }
}
