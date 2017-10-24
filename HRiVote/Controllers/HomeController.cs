using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Controllers
{
    public class HomeController : Controller
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
        // GET: Calendar

        public ActionResult Index(bool? candidate,bool? employee,bool? meetings,bool? projects)
        {
            var meets = db.sredbi.Where(x => x.MeetingDay.Day - DateTime.Now.Day <= -5).ToList();
            db.sredbi.RemoveRange(meets);
            db.SaveChanges();

            var calendar = new CalendarViewModel();
            if (candidate.HasValue)
            {
                calendar.Candidate = db.aplikanti.ToList();
            }
            if (employee.HasValue)
            {
                calendar.employees = db.emps.ToList();
            }
            if (meetings.HasValue)
            {
                calendar.Meetings = db.sredbi.ToList();

            }
            if (projects.HasValue)
            {
                calendar.Projects = db.project.ToList();
            }
            if (candidate.HasValue && employee.HasValue && meetings.HasValue && projects.HasValue)
            {
                calendar.Meetings = db.sredbi.ToList();
                calendar.Projects = db.project.ToList();
                calendar.employees = db.emps.ToList();
                calendar.Candidate = db.aplikanti.ToList();

            }
          
            return View("Index", calendar);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}