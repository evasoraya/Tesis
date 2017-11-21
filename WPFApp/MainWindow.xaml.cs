using Friends;
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
using Friends.Parsing;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModel vm;
        List<DataGrid> grids = new List<DataGrid>();

        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            this.DataContext = vm;
            
            grids.Add(this.RespuestaM);
            grids.Add(this.RespuestaP);
            grids.Add(this.RespuestaGEP);
            grids.Add(this.RespuestaGEM);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            vm.LimpiarError();

            String query = this.consulta.Text;
            if (!Validator.checkValidSql(query))
            {
                vm.MostrarError();
                return;
            }

            List<string> col = Friends.Parsing.SelectFields.getFields(query);
            foreach(DataGrid d in grids) d.Columns.Clear();

            foreach (DataGrid dg in grids)
            {
                foreach (string name in col)
                {
                    dg.Columns.Add(new DataGridTextColumn{
                        Header = name
                    });
                }
            }          
        }
    }
}
