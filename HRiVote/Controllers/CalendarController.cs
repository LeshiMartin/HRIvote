using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

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
        public ActionResult Open(Calendar calendar, HttpPostedFileBase file)
        {
            
            var viewmodel = new CalendarViewModel() { Employes = db.emps.Where(x=>x.IsAvailable==true).ToList() };
            if (calendar.Id == 0)
            {
                var title = TempData["title"].ToString();
                TempData.Keep("title");
                if (ModelState.IsValid)
                {
                   
                    calendar.Title = title;
                    if (calendar.Title == "Sick")
                    {
                        calendar.Color = "#bd180e";
                    }
                    if (calendar.Title == "Vacation")
                    {
                        calendar.Color = "#23a127";
                    }
                    if (calendar.Title == "Official Leave")
                    {
                        calendar.Color = "##bd180e";
                    }

                    if (calendar.EmployeeID!=null)
                    {
                        var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                        calendar.EmpName = employee.FullName;
                        calendar.Description = employee.FullName + " is on " + calendar.Title + " till : " + calendar.EndOfVacation.Value.ToShortDateString();
                        calendar.status = true;
                        if (file != null && file.ContentLength > 0)
                        {
                            var name = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Images"), name);
                            file.SaveAs(path);
                            EmployeeFiles files = new EmployeeFiles()
                            {
                                EmployeeID = employee.ID,
                                File = "~/Images/" + file.FileName,
                            };
                            db.empf.Add(files);
                            db.SaveChanges();
                        }
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
                    ModelState.AddModelError("", "Something went wrong please try again");
                    viewmodel.Calendar = calendar;
                    return View("OnLeaveDays", viewmodel);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (calendar.EmployeeID!=null)
                    {
                        var kalendar = db.kalendar.Single(x => x.Id == calendar.Id);
                        var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                        kalendar.EmployeeID = calendar.EmployeeID;
                        kalendar.EndOfVacation = calendar.EndOfVacation;
                        kalendar.StartOfVacation = calendar.StartOfVacation;
                        kalendar.Title = calendar.Title;
                        kalendar.EmpName = employee.FullName;
                        calendar.Description = employee.FullName + " is on " + calendar.Title + " till : " + calendar.EndOfVacation.Value.ToShortDateString();
                        if (calendar.Title == "Sick")
                        {
                            calendar.Color = "#bd180e";
                        }
                        if (calendar.Title == "Vacation")
                        {
                            calendar.Color = "#23a127";
                        }
                        if (calendar.Title == "Official Leave")
                        {
                            calendar.Color = "##bd180e";
                        }


                        kalendar.Color = calendar.Color;
                        kalendar.status = true;
                        if (file != null && file.ContentLength > 0)
                        {
                            var name = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Images"), name);
                            file.SaveAs(path);
                            EmployeeFiles files = new EmployeeFiles()
                            {
                                EmployeeID = employee.ID,
                                File = "~/Images/" + file.FileName,
                            };
                            db.empf.Add(files);
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index"); 
                    }
                    else
                    {
                        viewmodel.Calendar = calendar;
                        ModelState.AddModelError("", "The Input Couldn't be validated try again");
                        return View("OnLeaveDays", viewmodel);
                    }
                }
                else
                {
                    viewmodel.Calendar = calendar;
                    ModelState.AddModelError("", "The Input Couldn't be validated try again");
                    return View("OnLeaveDays", viewmodel);
                }
            }
        }
        public ActionResult Edit(int id)
        {
            var kalenadr = db.kalendar.Include(c=>c.employee).Single(x => x.Id == id);
            var emps = db.emps.Where(x => x.IsAvailable == true).ToList();
            emps.Add(kalenadr.employee);
            var viewmodel = new CalendarViewModel()
            {
                Calendar = kalenadr,
                Employes = emps
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
            var kalen = db.kalendar.Where(x => x.status == true).ToList();
            return new JsonResult { Data = kalen, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
      

    }
}