using AutoMapper;
using HRiVote.DAL;
using HRiVote.DTO;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Routing;

namespace HRiVote.Controllers.API
{
    
    public class EmployeeApiController : ApiController
    {
        private Entity db = new Entity();
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            return db.emps.ToList().Select(Mapper.Map<Employee, EmployeeDTO>);
        }
        public EmployeeDTO GetEmployee(int id)
        {
            var employee = db.emps.SingleOrDefault(c => c.ID == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Mapper.Map<Employee, EmployeeDTO>(employee);
        }
        [HttpPost]
        public void post([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            db.emps.Add(employee);
            db.SaveChanges();            
           
        }
        [HttpPut]
        public void UpdateEmployee(int id,[FromBody]Employee employe)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var employee = db.emps.Single(x => x.ID == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            employee.LastName = employe.LastName;
            employee.Achievements = employe.Achievements;
            employee.BirthDate = employe.BirthDate;
            employee.CV = employe.CV;
            employee.Email = employe.Email;
            employee.FirstName = employe.FirstName;
            employee.IsAvailable = employe.IsAvailable;
            employee.job = employe.job;
            employee.JobPositionID = employe.JobPositionID;
            employee.Phone = employe.Phone;
            employee.Photo = employe.Photo;
           // employee.Projects = employe.Projects;
            employee.VacationDays = employe.VacationDays;
            db.SaveChanges();
            throw new HttpResponseException(HttpStatusCode.OK);

        }
        [HttpDelete]
        public void DeleteEmployee (int id)
        {
            var employee = db.emps.FirstOrDefault(x => x.ID == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            db.emps.Remove(employee);
            db.SaveChanges();
        }

    }
}
