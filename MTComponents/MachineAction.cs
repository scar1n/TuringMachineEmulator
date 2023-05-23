using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    class MachineAction
    {
        public char ActionChar;
        private char CharForReplace = '1';
        private string Direction = "1"; //l_ - left, r_ - right, _ - steps count
        private string NextState = "1";

        public MachineAction(char actionChar)
        {
            ActionChar = actionChar;
        }
        public override string ToString() // primer '1'-l2-1
        {
            return $"{CharForReplace}-{Direction}-{NextState}";
        }
        public void OverrideAction(string actionStr)
        {
            ActionChar = actionStr.Split('-')[0][0];
            Direction = actionStr.Split('-')[1];
            NextState = actionStr.Split('-')[2];
        }
    }
}
