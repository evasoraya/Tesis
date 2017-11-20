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
            movie = new MovieModel();
        }

        private MovieModel movie;

        public MovieModel Movie { get => movie; set => movie = value; }

    }
}
