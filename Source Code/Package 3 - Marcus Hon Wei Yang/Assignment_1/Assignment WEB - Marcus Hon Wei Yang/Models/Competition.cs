using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class Competition
    {
        [Display(Name = "ID")]
        public int CompetitionId { get; set; }

        [Display(Name = "Competition Name")]
        public string CompetitionName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Result Released Date")]
        public DateTime ResultReleasedDate { get; set; }
    }
}
