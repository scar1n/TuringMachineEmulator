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
        public char CharForReplace = '1';
        public char Direction = 'l';
        public int StepsCount = 1;
        public int NextState = 1;

        public MachineAction(char actionChar)
        {
            ActionChar = actionChar;
        }
        public override string ToString() // primer '1'-l2-1
        {
            return $"{CharForReplace}-{Direction}{StepsCount}-{NextState}";
        }
        public void OverrideAction(string actionStr)
        {
            CharForReplace = actionStr.Split('-')[0][0];
            Direction = actionStr.Split('-')[1][0];
            StepsCount = int.Parse(actionStr.Split('-')[1][1].ToString());
            NextState = int.Parse(actionStr.Split('-')[2].ToString());
        }
    }
}
