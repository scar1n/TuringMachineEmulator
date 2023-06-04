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
