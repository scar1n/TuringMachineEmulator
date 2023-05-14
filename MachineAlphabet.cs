using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineEmulator
{
    static class MachineAlphabet
    {
        static List<char> Alphabet = new List<char>();
        public static void ResetAlphabet(string text)
        {
            Alphabet.Clear();
            foreach (char c in text)
            {
                Alphabet.Add(c);
            }
        }
        public static bool SymbolInAlphabet(char c)
        {
            return Alphabet.Contains(c);
        }
    }
}
