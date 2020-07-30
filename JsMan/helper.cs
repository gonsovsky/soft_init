using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsMan
{
    public static class Helper
    {
        public static bool IsNullOrEmptyJS(this string a)
        {
            return string.IsNullOrEmpty(a) || a.CompareTo("null") == 0 || a.CompareTo("undefined") == 0;
        }
    }
}
