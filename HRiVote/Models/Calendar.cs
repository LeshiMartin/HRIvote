
using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Calendar
    {
       
        public int Id { get; set; }
        [Required]
        public int? EmployeeID { get; set; }
        
        public Employee employee { get; set; }            
        [ScaffoldColumn(false)]
        [Required]
        public string Title { get; set; }
        [ScaffoldColumn(false)]
        public string Color { get; set; }
       
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [CalendarStart]
        public DateTime? StartOfVacation { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [CalendarEnd]
        public DateTime? EndOfVacation { get; set; }
        [ScaffoldColumn(false)]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool status { get; set; }
        public string EmpName { get; set; }
    }
}