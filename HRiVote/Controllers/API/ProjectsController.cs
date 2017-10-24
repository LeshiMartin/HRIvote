using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class ProjectsController : ApiController
    {
        private Entity db = new Entity();
        

        public IEnumerable<ProjectManagement> GetProject()
        {
            return db.project.ToList();
        }

        public ProjectManagement GetProjectManagement(int id)
        {
            var project = db.project.Single(x => x.ID == id);
            if (project == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            return project;
        }
        [HttpPost]
        public void CreateProject([FromBody]ProjectManagement projectManagement)
        {
            if (ModelState.IsValid)
            {
                db.project.Add(projectManagement);
                db.SaveChanges();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        [HttpPut]
        public void UpdateProject(int id,[FromBody]ProjectManagement projectManagement)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var project = db.project.Single(x => x.ID == id);
            if (project == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            project.Assignee = projectManagement.Assignee;
            project.Description = projectManagement.Description;
            project.EmployeeID = projectManagement.EmployeeID;
            project.EndDate = projectManagement.EndDate;
            project.File = projectManagement.File;
            project.ProjectName = projectManagement.ProjectName;
            project.StartDate = projectManagement.StartDate;
            project.Status = projectManagement.Status;
            db.SaveChanges();
            throw new HttpResponseException(HttpStatusCode.OK);
        }
        [HttpDelete]
        public void DeleteProject(int id)
        {
            var project = db.project.FirstOrDefault(x => x.ID == id);
            db.project.Remove(project);
            db.SaveChanges();
            throw new HttpResponseException(HttpStatusCode.OK);
        }
    }
}
