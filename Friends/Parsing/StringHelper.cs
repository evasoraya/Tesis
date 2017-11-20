using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Parsing
{
    public class StringHelper
    {

        public static String findTextBetweenTwoStrings(String haystack, String A, String B)
        {
            int pFrom = haystack.IndexOf(A) + A.Length;
            int pTo = haystack.LastIndexOf(B);
            String result = haystack.Substring(pFrom, pTo - pFrom);
            return result;
        }
    }
}
