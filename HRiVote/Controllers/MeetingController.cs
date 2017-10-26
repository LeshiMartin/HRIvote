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
using HRiVote.Filters;
using System.Data;
using System.Data.Entity.Validation;

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
        
       // [ValidateAntiForgeryToken]
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
                        if (meeting.MeetingTitle == null || meeting.MeetingTitle == "")
                        {
                            meeting.MeetingTitle = "Meeting for " + project.ProjectName;
                            db.SaveChanges();
                        }
                        project.MeetingID = meeting.ID;
                        try
                        {
                            // Your code...
                            // Could also be before try if you know the exception occurs in SaveChanges

                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                           
                        }
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
                    if (meeting.MeetingTitle == null || meeting.MeetingTitle == "")
                    {
                        meeting.MeetingTitle = "Meeting for " + project.ProjectName;
                    }
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