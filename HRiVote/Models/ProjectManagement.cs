using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class ProjectManagement
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string File { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndDate { get; set; }
        public int? EmployeeID { get; set; }
        public Employee Assignee { get; set; }
        public int? MeetingID{get;set;}
        public Meeting meeting { get; set; }
    }
}