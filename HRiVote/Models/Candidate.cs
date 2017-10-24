using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Candidate
    {
        public int ID { get; set; }

        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Е-маил"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "CV")]
        public string CV { get; set; }

        [Display(Name ="Датум на интервју")]
        [DataType(DataType.DateTime)]
        public DateTime InterviewDate { get; set; }

        [Display(Name ="Круг на интервју")]
        public int InterviewRound { get; set; }
    }
}