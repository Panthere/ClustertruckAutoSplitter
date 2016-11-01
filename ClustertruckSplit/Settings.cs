using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClustertruckSplit
{
    public static class Settings
    {
        public static int SleepTime;
        public static string PipeName;
        public static bool IsLevelMode;
        public static bool Pause;
        public static bool Reset;

        static Settings()
        {
            SleepTime = 10;
            PipeName = "LiveSplit";
            IsLevelMode = bool.Parse("LEVEL");
            Pause = bool.Parse("PAUSE");
            Reset = bool.Parse("RESET");
        }
    }
}
