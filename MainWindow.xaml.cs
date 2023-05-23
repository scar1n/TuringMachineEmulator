using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TuringMachine turingMachine;
        public MainWindow()
        {
            InitializeComponent();
            turingMachine = new TuringMachine();
            UpdateStateTable(StatesTableDG);
            UpdateTape(turingMachine.Tape);
        }
        private void UpdateTape(MachineTape tape)
        {
            int index = tape.CurrentCellNumber - ((TapeSP.Children.Count - 1) / 2);
            
            foreach (UserControls.MachineCell c in TapeSP.Children)
            {
                var temp = tape.GetCell(index++);
                c.Number = temp.CellNumber.ToString();
                c.CellValue = temp.CellValue.ToString();

                c.DataContext = new UserControls.MachineCell(temp.CellValue.ToString(), temp.CellNumber.ToString());
            }

        }
        private void SaveTape(MachineTape tape, MachineAlphabet alphabet) 
        {
            foreach (UserControls.MachineCell c in TapeSP.Children)
            {
                if (c.CellValue == "" || !(alphabet.SymbolInAlphabet(c.CellValue[0])))
                    c.cellValue.Text = "#";
                else
                    tape.ChageValue(int.Parse(c.Number), c.CellValue[0]);
            }
        }
        private void UpdateStateTable(DataGrid dg)
        {
            turingMachine.StateTable.ResetStatesActions(turingMachine.MachineAlphabet);

            dg.BeginInit();
            dg.DataContext = CreateDT(turingMachine).DefaultView;
            dg.EndInit();
        }
        private void AddStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.AddState(turingMachine.MachineAlphabet);
            UpdateStateTable(StatesTableDG);
        }
        private void DelStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.DeleteState();
            UpdateStateTable(StatesTableDG);
        }
        private void CarriegeStep_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.Tape.CarriageStep(1);
            UpdateTape(turingMachine.Tape);

        }
        private void CarriegeReturn_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.Tape.CarriageReturn(1);
            UpdateTape(turingMachine.Tape);
        }
        private void TapeSP_KeyUp(object sender, KeyEventArgs e)
        {
                SaveTape(turingMachine.Tape, turingMachine.MachineAlphabet);
        }
        private void AlphabetTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(AlphabetTB.Text == ""))
            {
                string alph = string.Empty;

                foreach (var item in AlphabetTB.Text.ToList().Distinct())
                    alph += item;

                AlphabetTB.Text = alph;

                turingMachine.MachineAlphabet.ResetAlphabet(AlphabetTB.Text);
                UpdateStateTable(StatesTableDG);
            }
        }
        private DataTable CreateDT(TuringMachine turingMachine)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Символ алфавита");

            foreach (var item in turingMachine.MachineAlphabet.Alphabet)
            {
                dt.Rows.Add(item);
            }

            foreach (var item in turingMachine.StateTable.States)
            {
                dt.Columns.Add($"Q{item.number}");

                foreach (var action in item.Actions)
                {
                    int index = turingMachine.MachineAlphabet.Alphabet.FindIndex(s => s == action.ActionChar);

                    dt.Rows[index][item.number + 1] = action.ToString();
                }
            }

            return dt;
        }
    }
}
