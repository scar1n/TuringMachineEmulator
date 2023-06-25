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
            StepsCount = 0;
            NextState = number;
        }
        public override string ToString() // primer '1'-l2-1
        {
            return $"{CharForReplace}-{Direction}{StepsCount}-{NextState}";
        }
        public void OverrideAction(string actionStr)
        {
            var splittedActionStr = actionStr.Split('-');


            CharForReplace = splittedActionStr[0][0];
            Direction = splittedActionStr[1][0];
            StepsCount = int.Parse(splittedActionStr[1][1].ToString());
            NextState = int.Parse(splittedActionStr[2].ToString());
        }
    }
}
