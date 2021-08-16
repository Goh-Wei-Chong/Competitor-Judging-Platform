using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class Criteria
    {
        [Display(Name = "ID")]
        public int CriteriaId { get; set; }


        [Display(Name = "Competition ID")]
        [Key]
        [ForeignKey("CompetitionId")]
        public int CompetitionId { get; set; }

        [Display(Name = "Competition Name")]
        public string CompetitionName { get; set; }


        [Display(Name = "Criteria")]
        [StringLength(50, ErrorMessage = "Criteria Name is too long!")]
        public string CriteriaName { get; set; }

        
        [Display(Name = "Weightage")]
        
        public int Weightage { get; set; }
    }
}
