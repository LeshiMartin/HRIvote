using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HRiVote.Controllers
{
    [HandleError(View = "DetailedError")]
    public class MeetingController : Controller
    {
        private Entity db = new Entity();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult CreateMeeting()
        {
            var ViewModel = new MeetingViewModel()
            {
                meeting = new Meeting(),
                Employees = db.emps.ToList(),
                projects = db.project.ToList()
            };
            return View(ViewModel);

        }
        public ActionResult Reschedule(int id)
        {
            var meet = db.sredbi.Single(x => x.ID == id);
            if (meet == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new MeetingViewModel()
            {
                meeting = meet,
                Employees = db.emps.ToList(),
                projects = db.project.ToList()
            };
            return View("CreateMeeting", viewmodel);
        }
        // GET: Meeting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Meeting meeting,int? Employes,int? Projects)
        {
            if (meeting.ID == 0)
            {              
                db.sredbi.Add(meeting);
                db.SaveChanges();
                if (Employes.HasValue)
                {
                    var emps = db.emps.Where(x => x.ID == Employes).ToList();
                    foreach (var item in emps)
                    {
                        item.MeetingID = meeting.ID;
                    }
                }
                if (Projects.HasValue)
                {
                    var projects = db.project.Where(x => x.ID == Projects).ToList();
                    foreach (var item in projects)
                    {
                        if (item.ID == Projects)
                        {
                            item.MeetingID = meeting.ID;
                        }
                    }
                }
                RedirectToAction("Index");
            }
            else
            {
                var meet = db.sredbi.Single(x => x.ID == meeting.ID);
                if (Employes.HasValue)
                {
                    var emps = db.emps.Where(x => x.ID == Employes).ToList();
                    foreach (var item in emps)
                    {
                        item.MeetingID = meeting.ID;
                    }
                }
                if (Projects.HasValue)
                {
                    var projects = db.project.Where(x => x.ID == Projects).ToList();
                    foreach (var item in projects)
                    {
                        if (item.ID == Projects)
                        {
                            item.MeetingID = meeting.ID;
                        }
                    }
                }
                meet.MeetingDay = meeting.MeetingDay;
               
                meet.MeetingTime = meeting.MeetingTime;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }
        public ActionResult Index()
        {
            var meeting = db.sredbi.ToList();
            return View(meeting);
        }
        public ActionResult Details(int id)
        {
            var meeting = db.sredbi.Single(x => x.ID == id);
            return View(meeting);
        }
    }
}