using System.Collections.Generic;
using System.Linq;

namespace TuringMachineEmulator.MTComponents
{
    class MachineStateTable
    {
        public List<MachineState> States = new List<MachineState>();
        public MachineStateTable(MachineAlphabet alph) 
        {
            AddState(alph);
        }
        public void AddState(MachineAlphabet alph)
        {
            States.Add(new MachineState(alph, States.Count));
        }

        public void DeleteState()
        {
            if (States.Count > 1) { States.Remove(States.Last()); }
        }
        
        public void ResetStatesActions(MachineAlphabet alph)
        {
            foreach (var state in States)
            {
                state.ResetActions(alph);
            }
        }
    }
}
