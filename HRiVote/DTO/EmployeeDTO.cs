using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }

        [Required]
        
        public string LastName { get; set; }

        [Required]
        
        public string FirstName { get; set; }

        public string FullName { get { return LastName + " " + FirstName; } }

        [Required]
        [UnderAge]
        [DataType(DataType.Date), ]
        public DateTime BirthDate { get; set; }

        [Required]
        
        public bool? IsAvailable { get; set; }
        [Vacations]
        
        public int VacationDays { get; set; }

        public string Projects { get; set; }

        public string Achievements { get; set; }



        
        public int JobPositionID { get; set; }

        [ DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        [ DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }
    }
}