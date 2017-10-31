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
    public class CalendarController : Controller
    {
        Entity db;
        public CalendarController()
        {
            db = new Entity();
        }
        // GET: Calendar
        public ActionResult OnLeaveDays( string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                TempData["title"] = title;
            }
            var viewmodel = new CalendarViewModel
            {
                Calendar = new Calendar(),
                Employes = db.emps.Where(x=>x.IsAvailable==true).ToList()
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Open(Calendar calendar)
        {
            var title = TempData["title"].ToString();
            TempData.Keep("title");
            var viewmodel = new CalendarViewModel() { Employes = db.emps.Where(x=>x.IsAvailable==true).ToList() };
            if (calendar.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    calendar.Title = title;
                    var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                    employee.IsAvailable = false;
                    db.kalendar.Add(calendar);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong please try again");
                    viewmodel.Calendar = calendar;
                    return View("OnLeaveDays", viewmodel);
                }
            }
            else
            {
                var kalendar = db.kalendar.Single(x => x.Id == calendar.Id);
                var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                employee.IsAvailable = false;
                kalendar.EmployeeID = calendar.EmployeeID;
                kalendar.EndOfVacation = calendar.EndOfVacation;
                kalendar.StartOfVacation = calendar.StartOfVacation;
                kalendar.Title = title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return new JsonResult { Data = db.kalendar.Include(x => x.employee).ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}