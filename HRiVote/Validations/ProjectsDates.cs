using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class ProjectsDates:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (Entity db=new Entity())
            {
                var project = validationContext.ObjectInstance as ProjectManagement;
                if (project.StartDate.Date < DateTime.Now.Date)
                {
                    return new ValidationResult("Wrong date entered");
                }
                if (project.StartDate.Year - DateTime.Now.Year > 1)
                {
                    return new ValidationResult("You entered invalid Date");
                }
                else
                {
                    return ValidationResult.Success;
                }

    }
        }
    }
}