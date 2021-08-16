using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitionSubmissionViewModel
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public int CompetitorId { get; set; }
        public string CompetitorName { get; set; }

        [Display(Name = "File Submitted")]
        public string? FileSubmitted { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime? DateTimeFileUpload { get; set; }

        [Display(Name="Appeal")]
        public string? Appeal { get; set; }

        [Display(Name = "Vote Count")]
        public int VoteCount { get; set; }

        [Display(Name = "Ranking")]
        public int? Ranking { get; set; }

        [Display(Name = "Total Score")]
        public int TotalScore { get; set; }

        
    }
}
