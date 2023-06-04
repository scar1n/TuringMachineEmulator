using System.Collections.Generic;
using System.Linq;

namespace TuringMachineEmulator.MTComponents
{
    class MachineState
    {
        public int number;
        public List<MachineAction> Actions = new List<MachineAction>();

        public MachineState(MachineAlphabet machineAlphabet, int number)
        {
            this.number = number;
            CreateActions(machineAlphabet);
        }
        public void ResetActions(MachineAlphabet machineAlphabet)
        {
            Actions.Clear();
            CreateActions(machineAlphabet);
        }
        private void CreateActions(MachineAlphabet machineAlphabet)
        {
            foreach (char item in machineAlphabet.Alphabet)
                Actions.Add(new MachineAction(number,item));
        }
        public List<string> GetActions()
        {
            List<string> output = new List<string>();  
            foreach (var s in Actions)
            {
                output.Add(s.ToString());
            }
            return output;
        }
        public MachineAction GetActualAction(char c)
        {
            return Actions.FirstOrDefault(s => s.ActionChar == c);
        }
        public void OverrideAction(char actionChar,string actionStr)
        {
            MachineAction? action = Actions.FirstOrDefault(s => s.ActionChar == actionChar);

            if (action != null)
                action.OverrideAction(actionStr);
        }
    }
}
