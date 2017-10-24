using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class UnderAge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;

            var age = DateTime.Now.Year - employee.BirthDate.Year;
            if (DateTime.Now.Month > employee.BirthDate.Month)
            {
                age--;
            }
            if (age >= 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The Employee is UnderAge");
            }
        }
    }
}