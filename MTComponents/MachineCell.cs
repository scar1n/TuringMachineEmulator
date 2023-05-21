using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    class MachineCell
    {
        public int CellNumber;
        public char CellValue;

        public MachineCell(int cellNumber)
        {
            CellNumber = cellNumber;
            CellValue = '#';
        }
    }
}
