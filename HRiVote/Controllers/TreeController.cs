using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using System.Data;
using System.Net;
using HRiVote.Filters;
using HRiVote.DAL;
using HRiVote.Models;
using System.IO;
using HRiVote.ViewModels;

namespace DojoTree.Controllers
{
    public class TreeController : Controller
    {
        private Entity db = new Entity();

        // GET     /Tree/Data/3
        public ActionResult Index(string name)
        {
            var notify = new Nottifications();
            var Kalendar = db.kalendar.Include(c=>c.employee).ToList();
            var Candidates = db.aplikanti.ToList();
            var openposs = db.pozicii.ToList();
            foreach(var item in Kalendar)
            {
                if (item.status == true && item.EndOfVacation.Value.Date <= DateTime.Now.Date)
                {
                    notify.Notifications.Add("The " + item.EmpName + " " + item.Title + " has Ended/Ending today please update his/hers status");
                }
            }
            foreach(var item in Candidates)
            {
                if (item.InterviewDate.Date == DateTime.Now.Date)
                {
                    notify.Notifications.Add("The Candidate " + item.FirstName + " " + item.LastName + " has Interview today");
                }
                if (item.InterviewDate.Date < DateTime.Now.Date)
                {
                    notify.Notifications.Add("The Candidate " + item.FirstName + " " + item.LastName + " Interview Passed please update the Status");
                }
            }
            foreach(var item in openposs)
            {
                if(item.Status==true && item.EndOfJobOpenning.Date <= DateTime.Now.Date)
                {
                    notify.Notifications.Add("The job Oppening : " + item.Name + " is ending today/has ended please update the status");
                }
            }

            string path = Server.MapPath("~/Managment/");
            List<string> picFolders = new List<string>();

           // if (Directory.GetFiles(path, "*").Length > 0)
                picFolders.Add(path);

            foreach (string dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                //if (Directory.GetFiles(dir, "*").Length > 0)
                    picFolders.Add(dir);
            }
            picFolders.RemoveAt(0);
            var viewmodel = new FileViewModel()
            {
                pics = picFolders,
                files = "~/Managment/" + name,
                name=name
            };
           
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file ,string files)
        {

            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath(files), name);
                file.SaveAs(path);
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "You must select a file for download";
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                var emps = db.emps.ToList();
                var candidate = db.aplikanti.ToList();
              foreach(var item in emps)
                {
                    if (item.CV.Contains(path))
                    {
                        item.CV = "No CV uploaded";
                    }
                    if (item.Photo.Contains(path))
                    {
                        item.Photo = "No photo uploaded";
                    }
                }
              foreach(var item in candidate)
                {
                    if (item.CV.Contains(path))
                    {
                        item.CV = "Cv not Uploaded";
                    }
                }
                db.SaveChanges();
                System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }
    }
}