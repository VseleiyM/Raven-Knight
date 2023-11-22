using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UI
{
    public class SVCParser
    {
        public List<List<string>> rows { get; private set; } = new List<List<string>>();
        public SVCParser(TextAsset csvFile)
        {
            ParseCsvFile(csvFile.text);
        }

        void ParseCsvFile(string csvText)
        {
            csvText = csvText.Replace("\r", "");
            string[] lines = csvText.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split('\t');

                List<string> row = new List<string>(columns);
                rows.Add(row);
            }
        }
    }
}