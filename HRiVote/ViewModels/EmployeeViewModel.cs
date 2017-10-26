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
        public List<SelectListItem> Lpositions { get; set; }
    }
}