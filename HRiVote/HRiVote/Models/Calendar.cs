using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Calendar
    { 
        public int Id { get; set; }

        public int? EmployeeID { get; set; }
        public Employee employee { get; set; }
       
        public int? CandidateID { get; set; }
        public Candidate candidate { get; set; }
        public string Title { get; set; }
        public DateTime? StartOfVacation { get; set; }
        public DateTime? EndOfVacation { get; set; }
    }
}