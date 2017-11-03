using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class CalendarEnd:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var calendar = validationContext.ObjectInstance as Calendar;
            if (!calendar.EndOfVacation.HasValue)
            {
                return new ValidationResult("You must select the End Date");
            }
            if (calendar.EndOfVacation.Value.Date < calendar.StartOfVacation.Value.Date && calendar.EndOfVacation.Value.Date < DateTime.Now.Date)
            {
                return new ValidationResult("The Date you entered is invalid Please try again");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}