using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HRiVote.ViewModels;
using HRiVote.Models;
using System.IO;
using System.Threading.Tasks;

namespace HRiVote.Controllers
{
    public class ProjectController : Controller
    {
        public Entity db = new Entity();
        // GET: Project
        public async Task<ActionResult> Index()
        {
            var projects = db.project.Include(x => x.Assignee);
            return View(await projects.ToListAsync());
        }
        public ActionResult AddProject()
        {
            var viewModel = new CustomProjects()
            {
                project = new ProjectManagement(),
                employees = db.emps.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProjectManagement project,HttpPostedFileBase file)
        {
            if (project.ID == 0)
            {
                if (file != null)
                {
                    var name = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), name);
                    file.SaveAs(path);
                    project.File = "~/Images/" + file.FileName;
                }
                else
                {
                    project.File = "Missing";

                }
                db.project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var proj = db.project.Single(x => x.ID==project.ID);
                if (file != null)
                {
                    var name = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), name);
                    file.SaveAs(path);
                    proj.File = "~/Images/" + file.FileName;
                }
                else
                {
                    proj.File = "Missing";

                }
                proj.Assignee = project.Assignee;
                proj.Description = project.Description;
                proj.EmployeeID = project.EmployeeID;
                proj.EndDate = project.EndDate;
                proj.ProjectName = project.ProjectName;
                proj.StartDate = project.StartDate;
                proj.Status = project.Status;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Edit(int id)
        {
            var project = db.project.Single(x => x.ID == id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomProjects()
            {
                project = project,
                employees = db.emps.ToList()
            };
            return View("AddProject", viewModel);
        }
        public ActionResult Details(int id)
        {
            var projects = db.project.Single(x => x.ID == id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }
    }
}