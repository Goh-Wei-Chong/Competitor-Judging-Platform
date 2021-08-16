using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models
{
    public class CompetitionJudge
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Required(ErrorMessage = "Judge ID is required")]
        [Display(Name = "Judge")]
        public int? JudgeID { get; set; }
    }
}
