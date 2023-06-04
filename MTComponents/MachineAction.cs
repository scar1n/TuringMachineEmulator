namespace TuringMachineEmulator.MTComponents
{
    class MachineAction
    {
        public char ActionChar;
        public char CharForReplace;
        public char Direction;
        public int StepsCount;
        public int NextState;

        public MachineAction(int number, char actionChar)
        {
            ActionChar = actionChar;
            CharForReplace = actionChar;
            Direction = 'r';
            StepsCount = 1;
            NextState = number;
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
