﻿using System.Collections.Generic;
using System.Linq;

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
                AddCellToStart();

            for (int i = 1; i <= rLenght; i++)
                AddCellToEnd();
        }
        public void CarriageStep(int count)
        {
            CurrentCellNumber += count;
        }
        public void CarriageReturn(int count)
        {
            CurrentCellNumber -= count;

        }
        public void ChangeCurrentCellValue(char value)
        {
            Tape.FirstOrDefault(s => s.CellNumber == CurrentCellNumber).CellValue = value;
        }

        public void AddCellToStart()
        {
            Tape.Insert(0,new MachineCell(Tape.FirstOrDefault().CellNumber - 1));
        }
        public void AddCellToEnd()
        {
            Tape.Add(new MachineCell(Tape.Last().CellNumber + 1));
        }
        public MachineCell GetCell(int number)
        {
            return Tape.FirstOrDefault(p => p.CellNumber == number);
        }
        public char GetCurrentValue()
        {
            return Tape.Find(p => p.CellNumber == CurrentCellNumber).CellValue;
        }
        public void ChageValue(int number, char value)
        {
            Tape[Tape.FindIndex(p => p.CellNumber == number)].CellValue = value;
        }
    }
}
