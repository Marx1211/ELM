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
        public Dictionary<string, string> recognizedAbbreviation { get; set; }
        public Abbreviations(string filename)
        {
            //Reads the abbreviations and the expanded forms into a dictionary.
            recognizedAbbreviation = File.ReadAllLines(filename).Select(line => line.Split('=')).ToDictionary(line => line[0], line => line[1]);
        }

        // Process the body of the message and check for abbreviations. If an abbreviation is found, expand it within "<>". It takes the body of the message as an argument and returns a string with the expanded abbreviations.
        public string ExpandAbbreviations(string message)
        {
            string expanded = "";
            //This line splits the message into single words and loose punctuation signs and puts them into an array for easier processing.
            string [] toExpand = Regex.Split(message, @"\p{Z}");
            foreach(var word in toExpand)
            {
                foreach(var abb in recognizedAbbreviation)
                {
                    if (word.Equals(abb.Key))
                    {
                        //The value in the array is replaced by the value + the expanded version.
                        toExpand[Array.IndexOf(toExpand, word)] = word + " <" + abb.Value + ">";
                    }
                }
            }
            //Add each word in the array to a new string and return it.
            foreach (var word in toExpand) expanded = string.Concat(expanded, word) + " ";
            return expanded;
        }
    }
}
