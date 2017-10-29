using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class Vacations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var emps = validationContext.ObjectInstance as Employee;
            if (emps.VacationDays > 0 && emps.VacationDays <= 20)
            {
                return ValidationResult.Success;
            }
            else
            {
                emps.VacationDays = 20;
                return ValidationResult.Success;
            }
        }
    }
}