using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Models
{
    public class ValidateOverWeightage : ValidationAttribute
    {
        private CriteriaDAL criteriaContext = new CriteriaDAL();

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            int weightage = Convert.ToInt32(value);
            Criteria criteria = (Criteria)validationContext.ObjectInstance;

            int competitionId = criteria.CompetitionId;

            if (criteriaContext.OverWeightage(weightage, competitionId))
                return new ValidationResult("The total weightage is more than a 100 for this competition");
            else
                return ValidationResult.Success;
        }

        }
    }

