using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_1.Models
{
    public class Judge
    {
        public int JudgeID { get; set; }
        public string JudgeName { get; set; }
        public string Salutation { get; set; }
        public int AreaInterestID { get; set; }
        public string EmailAddr { get; set; }
        public string Password { get; set; }
    }
}
