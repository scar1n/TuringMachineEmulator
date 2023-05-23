using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachineEmulator.MTComponents;

namespace TuringMachineEmulator
{
    class TuringMachine
    {
        public MachineTape Tape;
        public MachineStateTable StateTable;
        public MachineAlphabet MachineAlphabet;
        public TuringMachine()
        {
            Tape = new MachineTape(100);
            MachineAlphabet = new MachineAlphabet();
            StateTable = new MachineStateTable(MachineAlphabet);
        }
    }
}
