using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator.MTComponents
{
    public class MachineAlphabet
    {
        public List<char> Alphabet;
        public MachineAlphabet()
        {
            Alphabet = new List<char>();
        }
        public MachineAlphabet(MachineAlphabet alphabet) 
        {
            Alphabet = alphabet.Alphabet;
        }
        public void ResetAlphabet(string text)
        {
            Alphabet.Clear();
            foreach (char c in text)
            {
                Alphabet.Add(c);
            }
        }
        public bool SymbolInAlphabet(char c)
        {
            return Alphabet.Contains(c);
        }
        public override string ToString()
        {
            string str = string.Empty;
            foreach (char c in Alphabet)
            {
                str += c;
            }
            return str;
        }
    }
}
