using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class CriteriaViewModel
    {
        [Display(Name = "ID ")]
        public int CriteriaId { get; set; }

        [Display(Name = "Competition")]
        public string CompetitionName { get; set;  }

        public int Weightage { get; set;  }
    }
}
