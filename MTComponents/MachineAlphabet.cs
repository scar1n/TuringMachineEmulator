﻿using System.Collections.Generic;

namespace TuringMachineEmulator.MTComponents
{
    public class MachineAlphabet
    {
        public List<char> Alphabet;
        public MachineAlphabet()
        {
            Alphabet = new List<char> {'#'};
        }

        public void ResetAlphabet(string text)
        {
            Alphabet.Clear();

            foreach (char c in text)
                Alphabet.Add(c);

            Alphabet.Add('#');
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
