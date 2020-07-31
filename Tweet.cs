using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELM
{
    class Tweet : Message
    {
        public string Body { get; set; }

        public Tweet(string header, string sender, string body) : base(header, sender)
        {
            if (body.Length <= 140) Body = body;
            else throw new Exception("Message too long!");
        }
        //Check the tweet for either mentions or hashtags and return a list with the found values. 
        //Second argument will define wether it searches for mentions(0) or hashtags(1).
        public List<string> checkTweet(string body, int checkWhat)
        {
            List<string> list = new List<string>();
            char what;
            switch (checkWhat)
            {
                case 0:
                    what = '@';
                    break; 
                case 1:
                    what = '#';
                    break;
                default:
                    throw new Exception("Wrong parameter.");
            }
            //Splits the body into words, then checks wether those words are mentions or hashtags and adds them to the list.
            string[] toCheck = Regex.Split(body, @"\p{Z}");
            foreach(var word in toCheck)
            {
                if(word != "") if (word[0] == what) list.Add(word);
            }
            return list;
        }
    }
}
