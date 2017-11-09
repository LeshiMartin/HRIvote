using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HRiVote.ViewModels;

namespace DojoTree.Controllers
{

    public class HomeController : Controller
    {
        Entity db;
        public HomeController()
        {
            db = new Entity();
        }
        public ActionResult Index()
        {
            var employeefiles = db.empf.Include(c=>c.employee).ToList();
            return View(employeefiles);
        }
        [HttpPost]
        public ActionResult IndexPost(string Titles)
        {
            var empls = db.empf.Include(e=>e.employee).Where(x => x.Type == Titles).ToList();
            return View("Index", empls);
        }
        public ActionResult Edit(int id)
        {
            var emp = db.empf.Single(x => x.Id == id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new EmployeeFilesViewModel()
            {
                files = emp,
                emplloyees = db.emps.ToList()
            };
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult EditPost(HttpPostedFileBase file,EmployeeFiles emplo)
        {
            if (emplo.Id > 0)
            {
                var emp = db.empf.Single(x => x.Id == emplo.Id);
                Upload(file, emp);
                emp.EmployeeID = emplo.EmployeeID;
                emp.Type = emplo.Type;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }
        public void Upload(HttpPostedFileBase file,EmployeeFiles emplo)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Managment/Document's"), name);
                file.SaveAs(path);
                emplo.File = "~/Managment/Document's/" + file.FileName;
            }
        }
    }
}