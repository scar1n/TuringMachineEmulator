using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    class MachineAction
    {
        private char? ActionChar;
        private string Direction;
        private char? CharForReplace;
        private MachineState? NextState;
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
