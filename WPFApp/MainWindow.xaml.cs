﻿using Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WPFApp.VM;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            this.DataContext = vm;

          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> col = Friends.Parsing.SelectFields.getFields(this.consulta.Text);
            this.RespuestaM.Columns.Clear();

            foreach (string name in col)
            {
                this.RespuestaM.Columns.Add(new DataGridTextColumn
                {
                    Header = name
                });
            }
        }
    }
}
