using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_1.DAL;

namespace Assignment_1.Models
{
    public class AreaInterest
    {
        [Display(Name = "Area Interest ID")]
        public int AreaInterestID { get; set; }

        [Display(Name = "Interest Name")]
        [ValidateAreaInterestNameExists]
        public string Name { get; set; }
    }
}
