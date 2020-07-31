using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELM
{
    class Email : Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> URLs { get; set; }

        public Email(string header, string sender, string subject, string body) : base(header, sender)
        {
            if (subject.Length <= 20) Subject = subject;
            else throw new Exception("Subject too long!");

            if (body.Length <= 1028) Body = this.checkURLs(body);
            else throw new Exception("Message too long!");
        }
        //Checks wether an email contains a URL, then stores it in a quarantined list and replaces the url with "quarantined url"
        public string checkURLs(string body)
        {
            string URLS_checked = "";
            //Regex for a web page

            var matches = Regex.Matches(body, @"(http | ftp | https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?");
            URLs = matches.Cast<Match>().Select(match => match.Value).ToList();
            string[] toExpand = Regex.Split(body, " ");
            foreach (var word in toExpand)
            {
                foreach (var url in URLs)
                {
                    string check = url.Substring(1);
                    if (word.Equals(check))
                    {
                        //The value in the array is replaced by the quarantined message
                        toExpand[Array.IndexOf(toExpand, word)] = " <URL Quarantined> ";
                    }
                }
            }
            foreach (var word in toExpand) URLS_checked = string.Concat(URLS_checked, word) + " ";
            return URLS_checked;
        }
    }

}
