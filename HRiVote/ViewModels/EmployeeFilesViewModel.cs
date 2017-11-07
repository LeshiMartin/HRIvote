using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class EmployeeFilesViewModel
    {
        public EmployeeFiles files { get; set; }
        public IEnumerable<Employee> emplloyees { get; set; }
    }
}