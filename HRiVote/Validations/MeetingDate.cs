using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Validations
{
    public class MeetingDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (Entity db = new Entity())
            {
                var date = validationContext.ObjectInstance as Meeting;
                if (date.MeetingDay.Date < DateTime.Now.Date)
                {
                    return new ValidationResult("Wrong Date inserted please try again");
                }
                if (db.sredbi.Select(x => x.MeetingDay).Contains(date.MeetingDay)&&db.sredbi.Select(x=>x.MeetingTime).Contains(date.MeetingTime))
                {
                    return new ValidationResult("There is allready a meeting on this date");
                }
                else
                {
                    return ValidationResult.Success;
                } 
            }
        }
    }
}