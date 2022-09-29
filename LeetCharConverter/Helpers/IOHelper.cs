using LeetCharConverter.Classes;
using LeetCharConverter.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeetCharConverter.Helpers
{
    public static class IOHelper
    {
        static string _inputDirectory => $@"{Directory.GetCurrentDirectory()}\input";

        static string _outputDirectory => $@"{Directory.GetCurrentDirectory()}\output";

        static string _latinWordFile => $@"{Directory.GetCurrentDirectory()}\input\WordInLatin.txt";

        public static Dictionary<Latin, List<string>> GetInputFiles()
        {
            Dictionary<Latin, List<string>> latinLeet = new Dictionary<Latin, List<string>>();
            DirectoryInfo directoryInfo = new DirectoryInfo(_inputDirectory);
            foreach (Latin latin in EnumEx.GetValues<Latin>())
            {
                if (latin == Latin.None)
                    continue;

                var files = directoryInfo.GetFiles($"{latin}.txt", SearchOption.AllDirectories);
                if (files.Length == 0)
                    return new Dictionary<Latin, List<string>>();

                var lines = File.ReadLines(files[0].FullName);
                if (!lines.Any())
                    return new Dictionary<Latin, List<string>>();

                List<string> leets = new List<string>();
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        leets.Add(line);
                }
                if (!leets.Any())
                    return new Dictionary<Latin, List<string>>();
                else
                    latinLeet.Add(latin, leets);
            }

            return latinLeet;
        }

        public static LatinInputWord GetLatinInputWord(Dictionary<Latin, List<string>> latinLeet)
        {
            LatinInputWord latinWord = new LatinInputWord(latinLeet);

            var lines = File.ReadLines(_latinWordFile);
            if (!lines.Any() || string.IsNullOrWhiteSpace(lines.First()))
                return latinWord;

            var line = lines.First();
            latinWord.GetLatinChars(line);

            return latinWord;    
        }

        public static void WriteLeetWords(List<string> leetWords)
        {
            if (!Directory.Exists(_outputDirectory))
                Directory.CreateDirectory(_outputDirectory);

            using (StreamWriter writetext = new StreamWriter($@"{_outputDirectory}\Words_{DateTime.Now.ToString("ddMMyyyy_hhmmmss")}.txt"))
            {
                foreach (string word in leetWords)
                    writetext.WriteLine(word);
            }
        }
    }
}
