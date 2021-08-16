using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Models
{
    public class Competition : IValidatableObject
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Required(ErrorMessage = "Area of Interest ID is required")]
        [Display(Name = "Area of Interest ID")]
        public int AreaInterestID { get; set; }
        [Required(ErrorMessage = "Competition Name is required")]
        [Display(Name = "Competition Name")]
        [ValidateCompetitionNameExists]
        [StringLength(255, ErrorMessage = "Name must be below 255 characters")]
        public string CompetitionName { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Results release date is required")]
        [Display(Name = "Results Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ResultReleasedDate { get; set; }


        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            
            if (DateTime.Now > StartDate)
            {
                yield return new ValidationResult("Start Date cannot be before current date.");
            }
            else if (EndDate < StartDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date");
            }
            else if (ResultReleasedDate < EndDate)
            {
                yield return new ValidationResult("Results Release Date must be greater than End Date");
            }
        }
    }
}
