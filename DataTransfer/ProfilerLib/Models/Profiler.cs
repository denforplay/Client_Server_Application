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
                var sw1 = actionsPair.Key.GetElapsedTime();
                var sw2 = actionsPair.Value.GetElapsedTime();
                double firstTime = sw1.TotalMilliseconds;
                double secondTime = sw2.TotalMilliseconds;
                profileData.AddData(_firstKey, firstTime);
                profileData.AddData(_secondKey, secondTime);
            }

            return profileData;
        }
    }
}
