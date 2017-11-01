
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

        public int? EmployeeID { get; set; }
        
        public Employee employee { get; set; }            
        [ScaffoldColumn(false)]
       
        public string Title { get; set; }
        [ScaffoldColumn(false)]
        public string Color { get; set; }
       
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartOfVacation { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
       
        public DateTime? EndOfVacation { get; set; }
        [ScaffoldColumn(false)]
        public string Description { get; set; }
    }
}