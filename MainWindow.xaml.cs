﻿using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    public partial class MainWindow : Window
    {
        private TuringMachine turingMachine;
        private DispatcherTimer timer;
        bool _MachineIsWorking = false;
        const int stp = 5;
        public MainWindow()
        {
            InitializeComponent();
            turingMachine = new TuringMachine();
            UpdateTape(turingMachine.Tape);
            ResetStateTable();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Step);
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
            dt.Columns[0].ReadOnly = true;

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
            char actionSymbol = (e.Row.Item as DataRowView).Row.ItemArray[0].ToString()[0];
            string actionString = (e.EditingElement as TextBox).Text;

            try
            {
                var splittedActionStr = actionString.Split('-');

                if (!turingMachine.MachineAlphabet.SymbolInAlphabet(splittedActionStr[0][0]))
                    throw new Exception();
                
                if (splittedActionStr[1][0] != 'l' && splittedActionStr[1][0] != 'r')
                    throw new Exception();
                    
                int.Parse(splittedActionStr[1][1].ToString());

                if (turingMachine.StateTable.States.FirstOrDefault(s => s.number == int.Parse(splittedActionStr[2].ToString())) == null)
                    throw new Exception();

                if (turingMachine.MachineAlphabet.SymbolInAlphabet(actionSymbol))
                {
                    if (e.EditingElement as TextBox != null)
                        turingMachine.StateTable.States[column - 1].OverrideAction(actionSymbol, actionString);
                }
            }
            catch (Exception)
            {
                (e.EditingElement as TextBox).Undo();
                e.Cancel = true;
                MessageBox.Show("Некорретный ввод значений действия состояния");
            }
        }

        private void OneStepBNClick(object sender, RoutedEventArgs e)//
        {
            MachineStep();
        }

        private void ResetTapeBNClick(object sender, RoutedEventArgs e)
        {
            turingMachine.ResetTape();
            UpdateTape(turingMachine.Tape);
        }

    private void ResetCurrentStateBNClick(object sender, RoutedEventArgs e)
        {
            turingMachine.ResetState();
            stateLabel.Content = $"Текущее состояние: Q{turingMachine.CurrentState.number}";
        }

        private void MachineStartBN_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindowControlsEnable();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / stp);
            _MachineIsWorking = true;
            timer.Start();
        }
        private void Step(object sender, EventArgs e)
        {
            if (_MachineIsWorking)
            {
                MachineStep();
            }
            else
            {
                timer.Stop();
            }
        }
        void MachineStep()
        {
            turingMachine.Step();
            stateLabel.Content = $"Текущее состояние: Q{turingMachine.CurrentState.number}";
            UpdateTape(turingMachine.Tape);
        }

        private void MachineStopBN_Click(object sender, RoutedEventArgs e)
        {
            _MachineIsWorking = false;
            ChangeWindowControlsEnable();
        }
        private void ChangeWindowControlsEnable()
        {
            CarriegeReturn.IsEnabled = !CarriegeReturn.IsEnabled;
            CarriegeStep.IsEnabled = !CarriegeStep.IsEnabled;
            MachineStartBN.IsEnabled = !MachineStartBN.IsEnabled;
            MachineStopBN.IsEnabled = !MachineStopBN.IsEnabled;
            OneStepBN.IsEnabled = !OneStepBN.IsEnabled;
            ResetTapeBN.IsEnabled = !ResetTapeBN.IsEnabled;
            AddStateButton.IsEnabled = !AddStateButton.IsEnabled;
            DelStateButton.IsEnabled = !DelStateButton.IsEnabled;
            ResetCurrentStateBN.IsEnabled = !ResetCurrentStateBN.IsEnabled;
            StatesTableDG.IsEnabled = !StatesTableDG.IsEnabled;
            AlphabetTB.IsEnabled = !AlphabetTB.IsEnabled;
        }
    }
}
