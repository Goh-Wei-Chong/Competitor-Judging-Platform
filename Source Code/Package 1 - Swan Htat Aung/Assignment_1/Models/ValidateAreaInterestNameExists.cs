using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_1.DAL;

namespace Assignment_1.Models
{
    public class ValidateAreaInterestNameExists : ValidationAttribute
    {
        private AreaInterestDAL areaInterestContext = new AreaInterestDAL();
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            string name = Convert.ToString(value);
            AreaInterest areaInterest = (AreaInterest)validationContext.ObjectInstance;
            int id = areaInterest.AreaInterestID;
            if (areaInterestContext.IsAreaInterestNameExist(name, id))
            {
                return new ValidationResult
                    ("Area of interest name already exists!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
