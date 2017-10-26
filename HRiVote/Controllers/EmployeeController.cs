using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using HRiVote.ViewModels;
using HRiVote.Validations;

namespace HRiVote.Controllers
{
    [HandleError(View = "DetailedError")]
    public class EmployeeController : Controller
    {
        private Entity db = new Entity();
        // GET: Employee
        public ActionResult AddEmployee()
        {
            
            
            var viewmodel = new EmployeeViewModel()
            {
                employee = new Employee(),
                positions = db.positions.ToList(),
               
            };
            return View(viewmodel);
        }
        [HttpPost]       
        [ValidateAntiForgeryToken]
        [Checker]
        public async Task<ActionResult> Save(Employee employee, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (employee.ID == 0)
            {
                if (TryValidateModel(employee))
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var name = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), name);
                        file.SaveAs(path);
                        employee.CV = "~/Images/" + file.FileName;
                    }
                    else
                    {
                        employee.CV = "No CV uploaded";
                    }
                    if (file1!=null&&file1.ContentLength > 0)
                    {
                        var name1 = Path.GetFileName(file1.FileName);
                        var path1 = Path.Combine(Server.MapPath("~/Images"), name1);
                        file1.SaveAs(path1);
                        employee.Photo = "~/Images/" + file1.FileName;
                    }
                    else
                    {
                        employee.Photo = "No photo uploaded";
                    }
                    employee.FullName = employee.LastName + " " + employee.FirstName;
                    db.emps.Add(employee);
                    
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The user couldn't be validated try again if the error persist contact the administrator");
                    var viewmodel = new EmployeeViewModel()
                    {
                        employee = employee,
                        positions = db.positions.ToList()
                    };
                    return View("AddEmployee", viewmodel);
                }
            }
            var empl = db.emps.Single(x => x.ID == employee.ID);
            if (ModelState.IsValid)
            {
                empl.LastName = employee.LastName;
                empl.FirstName = employee.FirstName;
                empl.Email = employee.Email;
                empl.BirthDate = employee.BirthDate;
                empl.Achievements = employee.Achievements;
                empl.IsAvailable = employee.IsAvailable;
                empl.JobPositionID = employee.JobPositionID;
                empl.Phone = employee.Phone;
                empl.VacationDays = employee.VacationDays;
                empl.EmploymentStatus = employee.EmploymentStatus;
                if (file!=null&&file.ContentLength > 0)
                {
                    var name = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), name);
                    file.SaveAs(path); empl.CV = "~/Images/" + file.FileName;
                }
                else
                {
                    empl.CV = "No Cv Uploaded";
                }
                if (file1!=null&&file1.ContentLength > 0)
                {
                    var name1 = Path.GetFileName(file1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/Images"), name1);
                    file1.SaveAs(path1); empl.Photo = "~/Images/" + file1.FileName;
                }
                else
                {
                    empl.Photo = "No Photo Uploade";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewmode = new EmployeeViewModel()
            {
                employee = employee,
                positions = db.positions.ToList()
            };
            return View("AddEmployee", viewmode);
        }
        public ActionResult Edit(int id)
        {
            var emp = db.emps.Single(x => x.ID == id);
            if (emp == null)
            {
                return HttpNotFound();
            }
                var viewmodel = new EmployeeViewModel()
                {
                    employee = emp,
                    positions = db.positions.ToList(),

                };
                return View("AddEmployee", viewmodel);
            
        }
        public ActionResult Delete(int id)
        {
            var emp = db.emps.Include(x => x.job).Single(x => x.ID == id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleltePost(int id)
        {
            var emp = db.emps.Find(id);
            emp.EmploymentStatus = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public  ActionResult Index( )
        {
          
            return View(db.emps.Include(c=>c.job).Where(x=>x.EmploymentStatus==true).ToList());
        }

        public ActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AddPosition")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(JobPosition jobPosition,string skils)
        {
            if (ModelState.IsValid)
            {
                if (!db.positions.Select(x => x.Title).Contains(jobPosition.Title))
                {
                    jobPosition.Status = true;
                   
                    db.positions.Add(jobPosition);
                    db.SaveChanges();

                }
                else
                {
                    var job = db.positions.Single(x => x.Title == jobPosition.Title);
                    
                    db.SaveChanges();
                }
                return RedirectToAction("AddEmployee");
            }
            return View(jobPosition);
        }
        public ActionResult Details(int id)
        {
            var emp = db.emps.Include(c => c.job).Single(x => x.ID == id);
           

            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }
       

        
    }

}