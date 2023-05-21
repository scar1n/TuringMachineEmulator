using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    class MachineAction
    {
        private char ActionChar;
        private string? Direction; //l_ - left, r_ - right, _ - steps count
        private char? CharForReplace;
        private MachineState? NextState;
        public MachineAction(char actionChar)
        {
            ActionChar = actionChar;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
