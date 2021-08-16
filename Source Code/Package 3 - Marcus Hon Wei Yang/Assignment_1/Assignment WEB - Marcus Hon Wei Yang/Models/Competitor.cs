using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class Competitor
    {
        [Display(Name = "ID")]
        public int CompetitorId { get; set; }

        [Display(Name = "Name")]
        public string CompetitorName { get; set;  }

        [Display(Name = "Salutation")]
        public string Salutation { get; set; }

        [Display(Name = "EmailAddr")]
        public string EmailAddr { get; set; }
    }
}
