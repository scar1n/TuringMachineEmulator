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

namespace TuringMachineEmulator.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MachineCell.xaml
    /// </summary>
    public partial class MachineCell : UserControl
    {
        public MachineCell()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string CellValue { get; set; }
        
        public string Number { get; set; }

        private void cellValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cellValue.Text == "")
            {
                cellValue.Text = "#";
            }
        }
    }
}
