using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class CandidateDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var candidate = validationContext.ObjectInstance as Candidate;
            if (candidate.InterviewDate.Date < DateTime.Now.Date)
            {
                return new ValidationResult("The Date you enterd is invalid");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}