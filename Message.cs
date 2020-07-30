using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ELM
{
    public class Message
    {
        private string header;
        private string sender;
        public string Header { get { return header; } set { header = value; } }
        public string Sender { get { return sender; } set { sender = value; } }


        // This constructor performs the validation and throws an exception if the format is wrong. It takes two strings as arguments, @messageHeader is the message header and @messageSender is the sender.
        public Message(string messageHeader, string messageSender) {
            if (messageHeader.Length == 10)
            {
                if (messageHeader[0].Equals('S') | messageHeader[0].Equals('E') | messageHeader.Equals('T'))
                {
                    for (int i = 1; i < messageHeader.Length; i++)
                    {
                        bool check = Char.IsDigit(messageHeader[i]);
                        if (check != true) throw new Exception("Wrong header format!");
                    }
                    this.Header = messageHeader;
                }
                else throw new Exception("Wrong header format!");
            }
            else throw new Exception("Wrong header format!");
            switch (messageHeader[0])
            {
                case 'S':
                    if (messageSender[0] != '+') throw new Exception("Wrong phone format!");
                    if (messageSender.Length > 16) throw new Exception("Wrong phone format!");
                    this.Sender = messageSender;
                    break;
                case 'E':
                    var addr = new System.Net.Mail.MailAddress(messageSender);
                    if (addr.Address == messageSender) this.Sender = messageSender;
                    else throw new Exception("Wrong email format!");
                    break;
                case 'T':
                    if (messageSender[0] != '@') throw new Exception("Wrong tweet handle format!");
                    if (messageSender.Length > 16) throw new Exception("Wrong tweet handle format!");
                    this.Sender = messageSender;
                    break;
            }
                
        }
    }
}
