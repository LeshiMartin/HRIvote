using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class CalendarViewModel
    {
        public Calendar Calendar { get; set; }
        public IEnumerable<Employee> Employes { get; set; }
    }
}