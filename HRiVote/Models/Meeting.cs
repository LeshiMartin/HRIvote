using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    
    public class Meeting
    {
        [Key]
        public int ID { get; set; }       
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [MeetingDate]
        public DateTime MeetingDay { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString ="{0:hh-mm}")]
        public DateTime MeetingTime { get; set; }      
        
        public string MeetingTitle { get; set; } 
        public ICollection<Employee> emps { get; set; }
    }
}