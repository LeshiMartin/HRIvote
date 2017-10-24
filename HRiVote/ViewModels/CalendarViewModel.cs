using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<Employee> employees { get; set; }
        public IEnumerable<ProjectManagement> Projects { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public IEnumerable<Candidate> Candidate { get; set; }
    }
}