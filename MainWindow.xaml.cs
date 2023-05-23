using System;
using System.Collections.Generic;
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
            dg.BeginInit();

            dg.ItemsSource = from st in turingMachine.StateTable.States
                             select st.GetActions().ToList();
            var temp = from st in turingMachine.StateTable.States
                       select st.GetActions().ToList();
            dg.EndInit();
        }
        private void AlphabetTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (turingMachine.MachineAlphabet.SymbolInAlphabet(AlphabetTB.Text.Last())) 
            //{
            //    AlphabetTB.Text = AlphabetTB.Text.Remove(AlphabetTB.Text.Length - 1);
            //    return;
            //}
            //else
            //{
            //    turingMachine.MachineAlphabet.ResetAlphabet(AlphabetTB.Text);
            //}
            turingMachine.MachineAlphabet.ResetAlphabet(AlphabetTB.Text);
            turingMachine.StateTable.ResetStatesActions(turingMachine.MachineAlphabet);
            UpdateStateTable(StatesTableDG);
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
       
    }
}
