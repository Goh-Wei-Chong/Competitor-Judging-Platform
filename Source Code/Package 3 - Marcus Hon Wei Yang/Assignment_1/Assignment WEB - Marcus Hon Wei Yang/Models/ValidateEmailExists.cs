using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class ValidateEmailExists : ValidationAttribute

    {
        private JudgeDAL judgeContext = new JudgeDAL();
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string email = Convert.ToString(value);
            // Casting the validation context to the "Staff" model class
            Judge judge = (Judge)validationContext.ObjectInstance;
            // Get the Staff Id from the staff instance
            int judgeId = judge.JudgeId;
            if (judgeContext.IsEmailExist(email, judgeId))
                // validation failed
                return new ValidationResult
                ("Email address already exists!");
            else
                // validation passed
                return ValidationResult.Success;
        }
    }
}
