using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Parsing
{
    public class SelectFields
    {

        public static List<string> getFields(string query)
        {
            List<string> fields = new List<string>();
            String txtFields = StringHelper.findTextBetweenTwoStrings(query, "select", "from");
            String[] arrFields = txtFields.Split(',');
            for(int i = 0; i < arrFields.Length; i++)
                fields.Add(arrFields[i]);            
            return fields;
        }
    }
}
