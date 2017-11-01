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
                    if (calendar.Title == "Sick Leave")
                    {
                        calendar.Color = "#ff0000";
                    }
                    if (calendar.Title == "Vacation")
                    {
                        calendar.Color = "#00ff21";
                    }
                    if(calendar.Title=="Official Leave")
                    {
                        calendar.Color = "#00eaff";
                    }
                    
                    var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                    calendar.Description = employee.FullName + " is on " + calendar.Title + " till : " + calendar.EndOfVacation.Value.ToShortDateString();
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
        public ActionResult Edit(int id)
        {
            var kalenadr = db.kalendar.Single(x => x.Id == id);
            var viewmodel = new CalendarViewModel()
            {
                Calendar = kalenadr,
                Employes = db.emps.Where(x => x.IsAvailable == true).ToList()
            };
            return View("OnLeaveDays", viewmodel);
        }
        public ActionResult Index()
        {
            var viewmodel = new CalendarViewModel
            {
                Calendar = new Calendar(),
                Employes = db.emps.Where(x => x.IsAvailable == true).ToList()
            };
            return View(viewmodel);
        }
        public JsonResult GetData()
        {
            return new JsonResult { Data = db.kalendar.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveEvent(Calendar e)
        {
            var status = false;
            if (e.Id > 0)
                {
                    //Update the event
                    var v = db.kalendar.Where(a => a.Id == e.Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.Title = e.Title;
                        v.StartOfVacation = e.StartOfVacation;
                        v.EndOfVacation = e.EndOfVacation;
                        v.Description = e.Description;
                        v.EmployeeID = e.EmployeeID;
                        v.Color = e.Color;
                    }
                }
                else
                {
                    db.kalendar.Add(e);
                }
                db.SaveChanges();
            status = true;
            
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
           
            
                var v = db.kalendar.Where(a => a.Id == eventID).FirstOrDefault();
                if (v != null)
                {
                var employee = db.emps.Single(x => x.ID == v.EmployeeID);
                    employee.IsAvailable = true;
                    db.kalendar.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            
            return new JsonResult { Data = new { status = status } };
        }
        //public ActionResult Edit(int id)
        //{
        //    var viewmodel = new CalendarViewModel
        //    {
        //        Calendar = new Calendar(),
        //        Employes = db.emps.Where(x => x.IsAvailable == true).ToList()
        //    };

        //    return View("OnLeaveDays", viewmodel);
        //}

    }
}