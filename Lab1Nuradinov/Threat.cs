using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Nuradinov
{
    public class Threat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThreatSource { get; set; }
        public string ObjectOfInfluence { get; set; }
        public bool ConfidentialityViolation { get; set; }
        public string ConfidentialityViolationString 
        {
            get { return ConfidentialityViolation?  "Да" :  "Нет"; }
            set
            {
                if (value == "Да")
                {
                    ConfidentialityViolation = true;
                }
                else if (value == "Нет")
                {
                    ConfidentialityViolation = false;
                }
            }
        }
        public bool IntegrityViolation { get; set; }
        public string IntegrityViolationString
        {
            get { return IntegrityViolation ? "Да" : "Нет"; }
            set
            {
                if (value == "Да")
                {
                    IntegrityViolation = true;
                }
                else if (value == "Нет")
                {
                    IntegrityViolation = false;
                }
            }
        }
        public bool AccessViolation { get; set; }
        public string AccessViolationString 
        {
            get { return AccessViolation ? "Да" : "Нет"; }
            set 
            { if (value == "Да")
                {
                    AccessViolation = true;
                }
                else if(value == "Нет")
                {
                    AccessViolation = false;
                }
            }
        }
    }
}
