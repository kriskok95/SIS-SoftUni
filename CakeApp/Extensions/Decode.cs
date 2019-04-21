using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CakeApp.Extensions
{
    public static class Decode
    {
        public static string UrlDecode(this string input)
        {
            return WebUtility.UrlDecode(input);
        }
    }
}
