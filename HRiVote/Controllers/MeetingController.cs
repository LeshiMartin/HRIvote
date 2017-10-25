using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HRiVote.Validations;

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
                projects = db.project.Where(x=>x.MeetingID==0||x.MeetingID==null).ToList()
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
            var viewmodel = new MeetingViewModel()
            {
                meeting = meeting,
                Employees = db.emps.ToList(),
                projects = db.project.ToList(),
            };
            if (!Employes.HasValue || !Projects.HasValue)
            {
                return View("CreateMeeting", viewmodel);
            }
            else
            {
                if (meeting.ID == 0)
                {
                    if (ModelState.IsValid)
                    {
                        db.sredbi.Add(meeting);
                        db.SaveChanges();
                        var emplist = db.emps.Where(x => x.ID == Employes).ToList();
                        var project = db.project.SingleOrDefault(x => x.ID == Projects);
                        foreach(var item in emplist)
                        {
                            item.MeetingID = meeting.ID;
                        };
                        project.MeetingID = meeting.ID;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error try again");
                        return View("CreateMeeting", viewmodel);
                    }
                }
                else
                {
                    var meet = db.sredbi.Single(x => x.ID == meeting.ID);
                    var emplist = db.emps.Where(x => x.ID == Employes).ToList();
                    var project = db.project.SingleOrDefault(x => x.ID == Projects);
                    foreach (var item in emplist)
                    {
                        item.MeetingID = meeting.ID;
                    };
                    project.MeetingID = meeting.ID;
                    meet.MeetingDay = meeting.MeetingDay;
                    meet.MeetingTime = meeting.MeetingTime;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
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