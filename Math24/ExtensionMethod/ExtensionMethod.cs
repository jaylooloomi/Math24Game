using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Math24.ExtensionMethod
{
    public static class ExtensionMethod
    {
        public static string CustomToString(this int intValue)
        {
            if (intValue < 0)
            {
                return "(" + intValue.ToString() + ")";
            }

            return intValue.ToString();
        }
    }
}
