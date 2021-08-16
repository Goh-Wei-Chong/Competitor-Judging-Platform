using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitionScoreViewModel
    {
        [Display(Name = "Criteria")]
        public string CriteriaName { get; set; }

        [Display(Name = "Name")]
        public string CompetitorName { get; set;  }

        [Display(Name = "Competitor")]
        public int CompetitorId { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set;  }

    }
}
