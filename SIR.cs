using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELM
{
    class SIR : Email
    {
        public string Code { get; set; }
        public string NatureOfIncident { get; set; }

        private string[] NatureOfIncidentList = { "Theft of Properties", "Staff Attack",
                                                    "Device Damage","Raid",
                                                    "Customer Attack","Staff Abuse",
                                                    "Bomb Threat","Terorism",
                                                    "Suspicious Incident","Sport Injury",
                                                    "Personal Info Leak" };
        public SIR(string header, string sender, string subject, string body): base(header, sender, subject, body)
        {
            string handling = String.Join(" ", body.Split('\r','\n'));
            //Check for the sports centre code and the nature of incident. If either one is not present, throws exception.
            if (Regex.IsMatch(handling, @"[0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]"))
            {
                Code = Regex.Match(handling, @"[0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]").Value;
            }
            else throw new Exception("Wrong Format(No sports centre code)");

            foreach(var noi in NatureOfIncidentList)
            {
                if (handling.Contains(noi)) NatureOfIncident = noi;
                
            }
            if(NatureOfIncident.Length == 0) throw new Exception("Wrong Format(No Nature of Incident)");
        }
    }
}
