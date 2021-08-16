using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitionsViewModel
    {
        public List<Competitions> CompetitionsList { get; set; }      
        public List<CompetitionSubmission> CompetitionSubmissionList { get; set; }

        public CompetitionsViewModel()
        {
            CompetitionsList = new List<Competitions>();       
            CompetitionSubmissionList = new List<CompetitionSubmission>();
        }
    }
}
