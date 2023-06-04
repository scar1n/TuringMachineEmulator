using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    public partial class MainWindow : Window
    {
        private TuringMachine turingMachine;
        public MainWindow()
        {
            InitializeComponent();
            turingMachine = new TuringMachine();
            UpdateTape(turingMachine.Tape);
            ResetStateTable();
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
        private void ResetStateTable()
        {
            turingMachine.StateTable.ResetStatesActions(turingMachine.MachineAlphabet);
            UpdateStateTableDG(StatesTableDG);
        }
        private void UpdateStateTableDG(DataGrid dg)
        {
            dg.BeginInit();
            dg.DataContext = CreateDT(turingMachine).DefaultView;
            dg.EndInit();
        }
        private DataTable CreateDT(TuringMachine turingMachine)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Символ алфавита", typeof(char));


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
        private void AddStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.AddState(turingMachine.MachineAlphabet);
            UpdateStateTableDG(StatesTableDG);
        }
        private void DelStateButton_Click(object sender, RoutedEventArgs e)
        {
            turingMachine.StateTable.DeleteState();
            UpdateStateTableDG(StatesTableDG);
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
                ResetStateTable();
            }
        }
        private void StatesTableDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int column = e.Column.DisplayIndex;
            var test = (e.Row.Item as DataRowView).Row.ItemArray[0].ToString()[0];
            var test1 = (e.EditingElement as TextBox).Text;


            if (e.EditingElement as TextBox != null)
            {
                turingMachine.StateTable.States[column - 1].OverrideAction(
                    (e.Row.Item as DataRowView).Row.ItemArray[0].ToString()[0],
                    (e.EditingElement as TextBox).Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//
        {
            turingMachine.Step();
            stateLabel.Content = $"Текущее состояние: Q{turingMachine.CurrentState.number}";
            UpdateTape(turingMachine.Tape);
        }
    }
}
