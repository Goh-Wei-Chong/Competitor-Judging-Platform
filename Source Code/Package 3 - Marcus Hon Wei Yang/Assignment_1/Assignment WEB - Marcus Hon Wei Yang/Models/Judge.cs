using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class Judge
    {
        [Display(Name = "ID")]
        public int JudgeId { get; set; }


        [Required(ErrorMessage = "Please enter a name!")]
        [StringLength(50, ErrorMessage = "Please enter a name that does not exceed 50 characters!")]
        public string JudgeName { get; set;  }


        [StringLength(5, ErrorMessage ="Salutation too long")]
        public string Salutation { get; set; }


        //Foreign Key 
        [Display(Name = "Area of Interest")]
        public virtual int? AreaInterestId { get; set; }
       


        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Please enter your email!")]
        [StringLength(50, ErrorMessage = "Email Address is too long!")]
        [EmailAddress]
        // Custom Validation Attribute for checking email address exists
        [ValidateEmailExists]

        public string EmailAddr { get; set; }


        [Required(ErrorMessage="Please enter a password")]
        [StringLength(255, ErrorMessage = "Password is too long!")]
        public string Password { get; set; }

        public string AreaInterestName { get; set; }

        public IFormFile fileToUpload { get; set; }
    }

    
}
