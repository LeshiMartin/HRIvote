using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.Validations;
using HRiVote.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
<<<<<<< HEAD
                positions = db.positions.ToList(),
                skils = db.skilss.ToList()

=======
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList()
               
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Checker]
<<<<<<< HEAD
        public async Task<ActionResult> Save(Employee employee, HttpPostedFileBase file, HttpPostedFileBase file1, List<int> skills)
=======
        public async Task<ActionResult> Save(Employee employee, HttpPostedFileBase file, HttpPostedFileBase file1,List<int> skills)
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
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
                    if (file1 != null && file1.ContentLength > 0)
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
                    employee.EmploymentStatus = true;
                    List<Skills> skils = new List<Skills>();
<<<<<<< HEAD
                    foreach (var item in skills)
                    {
                        employee.skils = db.skilss.Where(x => x.ID == item).ToList();
                        if (!db.emps.Select(x => x.Email).Contains(employee.Email))
=======
                    foreach(var item in skills)
                    {
                        employee.skils = db.skilss.Where(x => x.ID == item).ToList();
                        if (!db.emps.Select(x=>x.Email).Contains(employee.Email))
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
                        {
                            db.emps.Add(employee);
                            await db.SaveChangesAsync();
                        }
<<<<<<< HEAD
                        else if (db.emps.Select(x => x.ID).Contains(employee.ID))
=======
                        else if(db.emps.Select(x=>x.ID).Contains(employee.ID))
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
                        {

                            var emp = db.emps.Single(x => x.ID == employee.ID);
                            emp.skils = employee.skils;
                            await db.SaveChangesAsync();
                        }
                        var sk = db.skilss.Single(x => x.ID == item);
                        skils.Add(sk);
<<<<<<< HEAD

                    }

=======
                       
                    }
                    
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
                    foreach (var item in skils)
                    {
                        item.Employees.Add(employee);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The user couldn't be validated try again if the error persist contact the administrator");
                    var viewmodel = new EmployeeViewModel()
                    {
                        employee = employee,
<<<<<<< HEAD
                        positions = db.positions.ToList(),
                        skils = db.skilss.ToList()
=======
                        positions = db.positions.Where(x => x.Status == true).ToList(),
                        skils =db.skilss.ToList()
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
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
                if (file != null && file.ContentLength > 0)
                {
                    var name = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), name);
                    file.SaveAs(path); empl.CV = "~/Images/" + file.FileName;
                }
                else
                {
                    empl.CV = "No Cv Uploaded";
                }
                if (file1 != null && file1.ContentLength > 0)
                {
                    var name1 = Path.GetFileName(file1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/Images"), name1);
                    file1.SaveAs(path1); empl.Photo = "~/Images/" + file1.FileName;
                }
                else
                {
                    empl.Photo = "No Photo Uploade";
                }
                List<Skills> skils = new List<Skills>();
                foreach (var item in skills)
                {
                    empl.skils = db.skilss.Where(x => x.ID == item).ToList();
                    await db.SaveChangesAsync();
                    var sk = db.skilss.Single(x => x.ID == item);
                    skils.Add(sk);
                }
                foreach (var item in skils)
                {
                    item.Employees.Add(employee);
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
<<<<<<< HEAD
            var viewmodel = new EmployeeViewModel()
            {
                employee = emp,
                positions = db.positions.ToList(),
                skils = db.skilss.ToList()

            };
            return View("AddEmployee", viewmodel);
=======
                var viewmodel = new EmployeeViewModel()
                {
                    employee = emp,
                    positions = db.positions.Where(x => x.Status == true).ToList(),
                    skils =db.skilss.ToList()
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9

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
<<<<<<< HEAD
        public ActionResult Index(bool? status)
=======
        public  ActionResult Index(bool? status )
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
        {
            List<Employee> emps = new List<Employee>();
            if (status.HasValue)
            {
<<<<<<< HEAD
                emps = db.emps.Include(c => c.job).Where(x => x.EmploymentStatus == status).ToList();
            }
            else
            {
                emps = db.emps.Include(c => c.job).ToList();
=======
                 emps = db.emps.Include(c => c.job).Where(x => x.EmploymentStatus == status).ToList();
            }
            else
            {
                 emps = db.emps.Include(c => c.job).ToList();
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
            }
            var viewmodel = new EmployeeViewModel()
            {
                emps = emps,
<<<<<<< HEAD
                positions = db.positions.ToList(),
=======
                positions = db.positions.Where(x=>x.Status==true).ToList(),
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
                skils = db.skilss.ToList(),
            };
            return View(viewmodel);
        }

        public ActionResult AddPosition()
        {
            var viewmodel = new PosstionSkilllViewModel()
            {
                jobPosition = new JobPosition(),
                skills = new Skills()
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ActionName("AddPosition")]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public ActionResult Add(JobPosition jobPosition, string skill)
=======
        public ActionResult Add(JobPosition jobPosition,string skill)
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
        {
            if (ModelState.IsValid)
            {
                if (!db.positions.Select(x => x.Title).Contains(jobPosition.Title))
                {
                    jobPosition.Status = true;
                    db.positions.Add(jobPosition);
                    db.SaveChanges();
                }
                if (!db.skilss.Select(x => x.Skill).Contains(skill))
                {
<<<<<<< HEAD
                    var skils = new Skills() { Status = true, Skill = skill };
                    db.skilss.Add(skils);
                    db.SaveChanges();

                }

=======
                    var skils = new Skills() { status = true, Skill = skill };
                    db.skilss.Add(skils);
                    db.SaveChanges();
                   
                }
               
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
                return RedirectToAction("AddEmployee");
            }
            return View(jobPosition);
        }
        public ActionResult Details(int id)
        {
<<<<<<< HEAD
            var emp = db.emps.Include(c => c.job).Include(x => x.skils).Single(x => x.ID == id);

=======
            var emp = db.emps.Include(c => c.job).Include(x=>x.skils).Single(x => x.ID == id);
           
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9

            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        public JsonResult GetData(int? skil,int? pos)
        {
            List<Employee> employees = new List<Employee>();
            if (skil.HasValue)
            {
                var emps = db.emps.Include(x=>x.job).Include(c=>c.skils).Where(x=>x.EmploymentStatus==true).ToList();
                emps = emps.Where(x => x.skils == db.skilss.Where(c => c.ID == skil).ToList()).ToList();
                employees = emps;
            }
            if (pos.HasValue)
            {
                var emps = db.emps.Include(x => x.job).Include(c => c.skils).Where(x => x.EmploymentStatus == true && x.JobPositionID == pos).ToList();
                employees = emps;
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

    }

}