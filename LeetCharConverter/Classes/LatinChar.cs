using LeetCharConverter.Enums;
using System.Collections.Generic;

namespace LeetCharConverter.Classes
{
    public class LatinLeetChar
    {
        public char Letter { get; }

        public List<string> LeetWords { get; }

        public int CountLeetWords => LeetWords.Count;

        public bool OpenLetter { get; }

        public LatinLeetChar(char letter, List<string> leetWords, bool openLetter = true)
        {
            Letter = letter;
            LeetWords = leetWords;
            OpenLetter = openLetter;
        }
    }
}
