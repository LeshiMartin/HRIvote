using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }
        public IEnumerable<JobPosition> positions { get; set; }
        public IEnumerable<Skills> skils { get; set; }
        public IEnumerable<Employee> emps { get; set; }
    }
}