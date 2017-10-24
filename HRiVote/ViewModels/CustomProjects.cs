using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class CustomProjects
    {
        public ProjectManagement project { get; set; }
        public IEnumerable<Employee> employees { get; set; }
    }
}