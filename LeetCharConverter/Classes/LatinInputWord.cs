using LeetCharConverter.Enums;
using System.Collections.Generic;

namespace LeetCharConverter.Classes
{
    public class LatinInputWord
    {
        public List<LatinLeetChar> LatinLeetChars { get; }
        Dictionary<Latin, List<string>> LatinLeet { get; }
        public bool Correct { get; set; }

        public LatinInputWord(Dictionary<Latin, List<string>> latinLeet)
        {
            LatinLeetChars = new List<LatinLeetChar>();
            LatinLeet = latinLeet;
            Correct = false;
        }

        public void GetLatinChars(string line)
        {
            bool bracket = false;
            foreach (char ch in line.ToCharArray())
            {
                if (ch == '[')
                {
                    bracket = true;
                    LatinLeetChars.Add(new LatinLeetChar(ch, new List<string>()));
                }
                if (ch == ']')
                {
                    bracket = false;
                    LatinLeetChars.Add(new LatinLeetChar(ch, new List<string>()));
                }
                if (char.IsLetter(ch))
                {
                    Latin latin = EnumEx.GetValueFromString<Latin>(ch.ToString().ToUpper());
                    LatinLeetChars.Add(new LatinLeetChar(ch, LatinLeet[latin], !bracket));
                }
            }
            if (line.Length == LatinLeetChars.Count)
                Correct = true;
        }
    }
}
