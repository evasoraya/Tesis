using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Model;

using Friends;

namespace WPFApp.VM
{
    public class ViewModel
    {

        public ViewModel()
        {
            queryError = "Hubo un error ejecutando el query...";
        }

        private String queryError;

        public String QueryError
        {
            get { return queryError; }
            set { queryError = value;  }
        }
    }
}
