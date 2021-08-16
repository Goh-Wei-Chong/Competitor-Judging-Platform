using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitionScore
    {
        [Display(Name = "Criteria ID")]
        public int CriteriaId { get; set; }

        [Display(Name = "Competitor ID")]
        public int CompetitorId { get; set; }

        [Display(Name = "Competition ID")]
        public int CompetitionId { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Competition")]
        public string CompetitionName { get; set;}

        [Display(Name = "Competitor")]
        public string CompetitorName { get; set; }

        [Display(Name = "Criteria")]
        public string CriteriaName { get; set;  }
    }
}
