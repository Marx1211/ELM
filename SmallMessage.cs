using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELM
{   
    class Abbreviations 
    {
        public Dictionary<string, string> recognizedAbbreviation { get { return recognizedAbbreviation; }set { recognizedAbbreviation = value} }
        public Abbreviations(string filename)
        {
            this.recognizedAbbreviation = File.ReadLines(filename).Select(line => line.Split('|')).ToDictionary(line => line[0], line => line[1]);
        }

        // Process the body of the message  
        public string ExpandAbbreviations(string message)
        {
            string expanded = "";
            string [] toExpand = Regex.Split(message, @"(?<=[.,;])");
            foreach(var word in toExpand)
            {
                foreach(var abb in recognizedAbbreviation)
                {
                    if (word.Equals(abb.Key))
                    {
                        toExpand[Array.IndexOf(toExpand, word)] = word + " <" + abb.Value + "> ";
                    }
                }
            }
            foreach (var word in toExpand) expanded = string.Concat(expanded, word);
            return expanded;
        }
    }
}
