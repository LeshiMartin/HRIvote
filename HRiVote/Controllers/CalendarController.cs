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
            
            var viewmodel = new CalendarViewModel()
            {
                Calendar = new Calendar(),
                Employes = db.emps.Where(x=>x.IsAvailable==true).ToList()
            };
            if (calendar.Id == 0)
            {
                if (calendar.EmployeeID != null || calendar.EmployeeID > 0)
                {
                    var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                    if (TempData["title"] != null)
                    {
                        var title = TempData["title"].ToString();
                        TempData.Keep("title");
                        calendar.Title = title;
                        Upload(file, employee, calendar);
                        calendar.description(employee);
                        if (ModelState.IsValid)
                        {
                            db.kalendar.Add(calendar);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("OnLeaveDays", viewmodel);
                        }
                    }
                    else
                    {
                        return View("OnLeaveDays", viewmodel);
                    }

                }


                else
                {
                    ModelState.AddModelError("", "You Must Select an Employee ");
                    return View("OnLeaveDays", viewmodel);
                }


            }

            else
            {
                if (ModelState.IsValid)
                {
                    if (calendar.EmployeeID != null)
                    {
                        var kalendar = db.kalendar.Single(x => x.Id == calendar.Id);
                        var employee = db.emps.Single(x => x.ID == calendar.EmployeeID);
                        kalendar.Update(calendar, kalendar);
                        kalendar.description(employee);
                        Upload(file, employee, calendar);
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

   
        public void Upload(HttpPostedFileBase file,Employee employee,Calendar calendar)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Managment/Document's"), name);
                file.SaveAs(path);
                EmployeeFiles files = new EmployeeFiles()
                {
                    EmployeeID = employee.ID,
                    File = "~/Managment/Document's/" + file.FileName,
                    Type = calendar.Title
                };
                db.empf.Add(files);
                db.SaveChanges();
            }
        }
  
      

    }
}