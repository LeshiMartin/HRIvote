using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Candidate
    {
        private Candidate candidate;

       

        public int ID { get; set; }

        [Required]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Е-маил"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

       // [Required]
        [Display(Name = "CV")]
        public string CV { get; set; }

        [Required]
        [CandidateDate]
        [Display(Name ="Датум на интервју")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InterviewDate { get; set; }

        [CandidateTime]
        [Display(Name = "Време на интервју")]
        [DataType(DataType.Time), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime? InterviewTime { get; set; }

        [Required]
        [Display(Name ="Круг на интервју")]
        public int InterviewRound { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> results = new List<ValidationResult>();

        //    if (InterviewDate < DateTime.Now || InterviewTime < DateTime.Now)
        //    {
        //        results.Add(new ValidationResult("Interview date and time must be greater than current time", new[] { "InterviewDate", "InterviewTime" }));
        //    }

        //    return results;
        //}
    }
}