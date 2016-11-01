using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPatcher
{
    public static class Extensions
    {
        public static int ToInt(this string text)
        {
            int data = 0;
            if (!int.TryParse(text, out data))
            {
                return 10;
            }
            return data;
        }
        public static float ToFloat(this string text)
        {
            float data = 0;
            if (!float.TryParse(text, out data))
            {
                return -1;
            }
            return data;
        }
        public static bool IsEmpty(this string a)
        {
            return string.IsNullOrEmpty(a);
        }
    }
}
