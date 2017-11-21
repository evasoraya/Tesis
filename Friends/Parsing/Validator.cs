using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Friends.Parsing
{
    public class Validator
    {

        public static bool checkValidSql(string query)
        {
            if (!query.StartsWith("select"))
                return false;

            List<String> mandatoryClauses = new List<String>();
            mandatoryClauses.Add("select");
            mandatoryClauses.Add("from");

            foreach(String clause in mandatoryClauses)
            {
                if (!StringHelper.appearsExactlyOnce(query, clause))
                    return false;
            }

            List<String> clauses = new List<String>();
            clauses.Add("select");
            clauses.Add("from");
            clauses.Add("where");
            clauses.Add("group by");
            clauses.Add("having");
            clauses.Add("order by");
            clauses.Add("limit");

            foreach (String clause in clauses)
            {
                if (!StringHelper.appearsAtMostOnce(query, clause))
                    return false;
            }

            int last = -1;
            foreach(String clause in clauses)
            {
                int currentAppereance = query.IndexOf(clause);
                if (currentAppereance == -1) continue;
                if (currentAppereance < last)
                    return false;
                last = currentAppereance;
            }

            return true;
        }
    }
}
