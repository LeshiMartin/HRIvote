using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Име и презиме")]
        [ScaffoldColumn(false)]
        public string FullName { get; set; }

        [Required]
        [UnderAge]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"), Display(Name = "Датум на раѓање")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Достапен")]
        public bool IsAvailable { get; set; }
        [Vacations]
        [Display(Name = "Денови од одмор")]
        public int VacationDays { get; set; }

        public bool EmploymentStatus { get; set; }

        public string Achievements { get; set; }

        public ICollection<Skills> skils { get; set; }

        [Display(Name = "Позиција")]
        public JobPosition job { get; set; }
        public int JobPositionID { get; set; }

        [Display(Name = "Телефон"), DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }
       
        [Required]
        [Display(Name = "Е-маил"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }
        
    }
}