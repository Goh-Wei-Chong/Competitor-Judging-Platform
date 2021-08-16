using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_1.DAL;

namespace Assignment_1.Models
{
    public class ValidateCompetitionNameExists : ValidationAttribute
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            string competitionName = Convert.ToString(value);
            Competition competition = (Competition)validationContext.ObjectInstance;
            int competitionID = competition.CompetitionID;
            if (competitionContext.IsCompetitionNameExist(competitionName, competitionID))
            {
                return new ValidationResult
                    ("Competition name already exists!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
