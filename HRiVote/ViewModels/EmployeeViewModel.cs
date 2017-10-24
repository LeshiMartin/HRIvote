using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }
        public IEnumerable<JobPosition> positions { get; set; }
    }
}