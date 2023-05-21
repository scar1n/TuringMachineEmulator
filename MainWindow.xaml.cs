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
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TuringMachine turingMachine ;
        public MainWindow()
        {
            InitializeComponent();
            turingMachine = new TuringMachine();
            ResetStateTable();
        }
        private void ResetStateTable()
        {
            StatesTableDG.BeginEdit();
            StatesTableDG.ItemsSource = turingMachine.StateTable.States;
            StatesTableDG.EndInit();
        }
        public void AlphabetTB_TextChanged(object sender, TextChangedEventArgs e)
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
        }

        private void AddStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.AddState(turingMachine.MachineAlphabet);
            ResetStateTable();
        }

        private void DelStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.DeleteState();
            ResetStateTable();
        }
    }
}
