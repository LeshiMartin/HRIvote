using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class EmployeeFiles
    {
        public int Id { get; set; }
        [Required]
        public string File { get; set; }
        public int EmployeeID { get; set; }
        public Employee employee { get; set; }
    }
}