
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
        
        public int? EmployeeID { get; set; }
        
        public Employee employee { get; set; }            
        [ScaffoldColumn(false)]
        
        public string Title { get; set; }
        [ScaffoldColumn(false)]
        public string Color { get;private set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [CalendarStart]
        [Required]
        public DateTime? StartOfVacation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [CalendarEnd]
        [Required]
        public DateTime? EndOfVacation { get; set; }
        [ScaffoldColumn(false)]
        public string Description { get;private set; }
        [ScaffoldColumn(false)]
        public bool status { get; private set; }
        public string EmpName { get; private set; }
        public Calendar()
        {

        }
        public void description(Employee employee)
        {

            if (Title == "Sick")
            {
               Color = "#bd180e";
            }
            if (Title == "Vacation")
            {
                Color = "#23a127";
            }
            if (Title == "Official Leave")
            {
                Color = "#345578";
            }

            EmpName = employee.FullName;
            if (EndOfVacation.HasValue)
            {
                Description = employee.FullName + " is on " + Title + " till : " + EndOfVacation.Value.ToShortDateString();
            }
            status = true;
        }
        public void Update(Calendar oldCalendar,Calendar NewCalendar)
        {
            NewCalendar.Title = oldCalendar.Title;
            NewCalendar.StartOfVacation = oldCalendar.StartOfVacation;
            NewCalendar.EmployeeID = oldCalendar.EmployeeID;
            NewCalendar.EndOfVacation = oldCalendar.EndOfVacation;
        }
        public void Remove()
        {
            status = false;
        }
    }
}