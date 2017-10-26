using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Skills
    {
        public int ID { get; set; }
        public string Skill { get; set; }
        public int? EmployeeID { get; set; }
        public Employee employee { get; set; }
    }
}