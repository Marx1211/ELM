using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM
{
    class SMS : Message
    {
        public string Body { get; set; }

        public SMS(string header, string sender, string body) : base(header, sender)
        {
            //Check for length over 140
            if (body.Length <= 140) Body = body;
            else throw new Exception("Message too long!");
        }
    }
}
