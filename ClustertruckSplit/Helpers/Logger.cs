using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClustertruckSplit
{
    public static class Logger
    {
        public static void Log(string data)
        {
            Log(data, null);
            //File.AppendAllText("SplitterLog.txt", data);
        }

        public static void Log(string data, params object[] items)
        {
            string logText;
            if (items.Length > 0 && items[0] != null)
            {
                logText = string.Format(data, items);
            }
            else
            {
                logText = data;
            }
            File.AppendAllText("SplitterLog.txt", logText);
        }
    }
}
