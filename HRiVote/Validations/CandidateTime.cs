using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class CandidateTime:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var candidate = validationContext.ObjectInstance as Candidate;
            if (!candidate.InterviewTime.HasValue)
            {
                return new ValidationResult("You Must enter an InterviewTime");
            }
            if (candidate.InterviewTime.Value.Hour < DateTime.Now.Hour)
            {
                return new ValidationResult("You entered invalid time for interview");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}