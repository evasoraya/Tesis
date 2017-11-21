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

        public static bool appearsExactlyOnce(String haystack, String needle)
        {
            int firstAppearence = haystack.IndexOf(needle);
            if (firstAppearence == -1) return false;
            firstAppearence += needle.Length;
            if (haystack.IndexOf(needle, firstAppearence) == -1) return true;
            return false;
        }

        public static bool appearsAtMostOnce(String haystack, String needle)
        {
            int firstAppearence = haystack.IndexOf(needle);
            if (firstAppearence == -1) return true;
            firstAppearence += needle.Length;
            if (haystack.IndexOf(needle, firstAppearence) == -1) return true;

            return false;
        }
    }
}
