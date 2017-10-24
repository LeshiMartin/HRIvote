using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class MeetingViewModel
    {
        public Meeting meeting { get; set; }
        public IEnumerable<ProjectManagement> projects { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}