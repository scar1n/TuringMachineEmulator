using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    internal class MachineTape
    {
        private List<MachineCell> Tape = new List<MachineCell>();
        public int CurrentCellNumber;

        public void CarriageStep()
        {

        }
        public void CarriageReturn()
        {

        }
        public void ChangeCurrentCellValue(char value)
        {

        }
    }
}
