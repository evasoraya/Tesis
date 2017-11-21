using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Friends;
using System.ComponentModel;

namespace WPFApp.VM
{
    public class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
        }

        private String queryError;

        public String QueryError
        {
            get { return queryError; }
            set {
                queryError = value;
                OnPropertyChanged("QueryError");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void LimpiarError()
        {
            QueryError = "";
        }

        public void MostrarError()
        {
            QueryError = "Hubo un error ejecutando el query...";
        }
    }
}
