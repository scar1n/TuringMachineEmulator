using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    class MachineTape
    {
        private List<MachineCell> Tape = new List<MachineCell>();
        public int CurrentCellNumber = 0;
        
        public MachineTape(int length)
        {
            int lLenght = (length - 1) / 2;
            int rLenght = length - 1 - lLenght;

            Tape.Add(new MachineCell(0));

            for (int i = 1; i <= lLenght; i++)
            {
                AddCellToStart();
            }
            for (int i = 1; i <= rLenght; i++)
            {
                AddCellToEnd();
            }

        }
        public void CarriageStep(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CurrentCellNumber++;
            }
        }
        public void CarriageReturn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CurrentCellNumber--;
            }
        }
        public void ChangeCurrentCellValue(char value)
        {
            Tape.FirstOrDefault(s => s.CellNumber == CurrentCellNumber).CellValue = value;
        }
        
        private void AddCellToStart()
        {
            Tape.Insert(0,new MachineCell(Tape.FirstOrDefault().CellNumber - 1));
        }
        private void AddCellToEnd()
        {
            Tape.Add(new MachineCell(Tape.Last().CellNumber + 1));
        }
    }
}
