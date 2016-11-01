using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPatcher
{
    public class PatchSettings
    {
        public string DllPath;
        public string TypeName;
        public string MethodName;
        public string PipeName;

        public int SleepTime;

        public bool LevelModeEnabled;
        public bool ResetEnabled;
        public bool PauseEnabled;
    }
}
