using System.Linq;
using System.Windows;
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    class TuringMachine
    {
        public MachineTape Tape;
        public MachineStateTable StateTable;
        public MachineAlphabet MachineAlphabet;
        public MachineState CurrentState;
        public TuringMachine()
        {
            Tape = new MachineTape(100);
            MachineAlphabet = new MachineAlphabet();
            StateTable = new MachineStateTable(MachineAlphabet);
            CurrentState = StateTable.States[0];
            MessageBox.Show("Всё загрузилось");
        }
        public void Step()
        {
            MachineAction CurrentAction = CurrentState.GetActualAction(Tape.GetCurrentValue());

            Tape.ChangeCurrentCellValue(CurrentAction.CharForReplace);

            if (CurrentAction.Direction == 'r')
                Tape.CarriageStep(CurrentAction.StepsCount);
            else if (CurrentAction.Direction == 'l')
                Tape.CarriageReturn(CurrentAction.StepsCount);

            if (CurrentAction.NextState != CurrentState.number)
            {
                CurrentState = StateTable.States.FirstOrDefault(s => s.number == CurrentAction.NextState);
            }
        }
    }
}
