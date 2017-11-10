using HRiVote.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Име и презиме")]
        [ScaffoldColumn(false)]
        public string FullName { get; set; }

        [Required]
        [UnderAge]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
           [ Display(Name = "Датум на раѓање")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Достапен")]
        public bool IsAvailable { get; set; }
        [Vacations]
        [Display(Name = "Денови од одмор")]
        public int VacationDays { get; set; }

        public bool EmploymentStatus { get; set; }

        public string Achievements { get; set; }

        public virtual ICollection<Skills> skils { get;private set; }

        [Display(Name = "Позиција")]
        public JobPosition job { get; set; }
        public int JobPositionID { get; set; }

        [Display(Name = "Телефон"), DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }
       
        [Required]
        [Display(Name = "Е-маил"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }

        public Employee()
        {
            skils = new Collection<Skills>();
                    
        }
     
        public void Created(Employee emps)
        {
            
            emps.FullName = emps.LastName + " " + emps.FirstName;
            emps.EmploymentStatus = true;
            

        }
        public void Updated(Employee oldEMployee,Employee newEmployee)
        {
            newEmployee.Achievements = oldEMployee.Achievements;
            newEmployee.BirthDate = oldEMployee.BirthDate;
            newEmployee.Email = oldEMployee.Email;
            newEmployee.FirstName = oldEMployee.FirstName;
            newEmployee.IsAvailable = oldEMployee.IsAvailable;
            newEmployee.JobPositionID = oldEMployee.JobPositionID;
            newEmployee.LastName = oldEMployee.LastName;
            newEmployee.FullName = newEmployee.LastName + " " + newEmployee.FirstName;
            newEmployee.Phone = oldEMployee.Phone;
            newEmployee.VacationDays = oldEMployee.VacationDays;
            if(oldEMployee.CV!= "No CV uploaded")
            {
                newEmployee.CV = oldEMployee.CV;
            }
            if(oldEMployee.Photo!= "No photo uploaded")
            {
                newEmployee.Photo = oldEMployee.Photo;
            }
         
        }
        public void Remove()
        {
            var emp = new Employee();
            emp.EmploymentStatus=false;
        }

        
    }
}