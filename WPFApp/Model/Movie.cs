using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Model
{
    public class MovieModel
    {

        #region Campos
        private int year;
        private string title;
        private string language;
        #endregion


        #region Propiedades
        public int Year { get => year; set => year = value; }
        public string Title { get => title; set => title = value; }
        public string Language { get => language; set => language = value; }
        #endregion

    }
}
