using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_1.Models
{
    public class CompetitionSubmission
    {
        public int CompetitionID { get; set; }
        public int CompetitorID { get; set; }
        public string FileSubmitted { get; set; }
        public DateTime DateTimeFileUpload { get; set; }
        public string Appeal { get; set; }
        public int VoteCount { get; set; }
        public int Ranking { get; set; }
    }
}
