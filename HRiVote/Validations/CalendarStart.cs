using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class CalendarStart:ValidationAttribute
    {
        Entity db;
        public CalendarStart()
        {
            db = new Entity();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var calendar = validationContext.ObjectInstance as Calendar;
            if (calendar.StartOfVacation.HasValue&&calendar.StartOfVacation.Value.Date < DateTime.Now.Date)
            {
                return new ValidationResult("The date you entered is wrong please try again");

            }
            if (!calendar.StartOfVacation.HasValue)
            {
                return new ValidationResult("You must choose a starting date");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}