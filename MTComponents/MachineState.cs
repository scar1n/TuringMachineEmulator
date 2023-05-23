using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Actions.Add(new MachineAction(item));
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
    }
}
