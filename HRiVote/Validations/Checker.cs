using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class Checker : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var emp = validationContext.ObjectInstance as Employee;
            using (Entity context = new Entity())
            {
                if (context.emps.Select(x => x.Email).Contains(emp.Email))
                {
                    return new ValidationResult("There is allreadt an Employee wiht this email account");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}