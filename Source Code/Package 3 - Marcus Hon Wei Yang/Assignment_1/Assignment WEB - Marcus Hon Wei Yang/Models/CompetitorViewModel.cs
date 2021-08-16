using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CompetitorViewModel
    {
        [Display(Name = "ID")]
        public int CompetitorId { get; set; }

        [Display(Name = "CompetitorName")]
        public string CompetitorName { get; set;  }
    }
}
