using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class EmployeesViewModel
    {
        public IEnumerable<Employee> employee { get; set; }
        public Employee emp { get; set; }
    }
}