using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitionViewModel
    {
        public List<Competition> CompetitionList { get; set; }      
        public List<CompetitionScore> CompetitionScoreList { get; set; }
        public List<CompetitionSubmission> CompetitionSubmission { get; set;  }

        public CompetitionViewModel()
        {
            CompetitionList = new List<Competition>();
           
            CompetitionScoreList = new List<CompetitionScore>();
            CompetitionSubmission = new List<CompetitionSubmission>();
        }
    }
}
