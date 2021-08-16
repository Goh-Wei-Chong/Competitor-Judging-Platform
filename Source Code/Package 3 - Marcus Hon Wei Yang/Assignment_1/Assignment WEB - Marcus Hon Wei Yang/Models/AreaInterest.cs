using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_WEB___Marcus_Hon_Wei_Yang;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class AreaInterest
    {
        [Display(Name ="ID")]
        public int AreaInterestId { get; set; }

        [Display(Name = "Area of Interest")]
        public string AreaInterestName { get; set; }
    }
}
