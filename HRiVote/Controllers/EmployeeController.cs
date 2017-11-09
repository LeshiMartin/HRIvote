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
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList()

            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Checker]
        public  ActionResult Save(Employee employee, HttpPostedFileBase file, HttpPostedFileBase file1, List<int> skills)
        {
            var viewmodel = new EmployeeViewModel()
            {
                employee = employee,
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList()
            };
            List<Skills> skils = new List<Skills>();
            if (employee.ID == 0)
            {
                if (TryValidateModel(employee))
                {
                    Upload(file, file1, employee);
                    employee.Created(employee);
                    if (skills!=null)
                    {
                        foreach (var item in skills)
                        {
                            skils.Add(db.skilss.Single(x => x.ID == item));
                            foreach (var tag in skils)
                            {
                                if (!employee.skils.Contains(tag))
                                {
                                    employee.skils.Add(tag);
                                    tag.Employees.Add(employee);
                                }
                            }
                        } 
                    }
                    db.emps.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The user couldn't be validated try again if the error persist contact the administrator");
                  
                    return View("AddEmployee", viewmodel);
                }
            }
            var empl = db.emps.Single(x => x.ID == employee.ID);
             
            if (ModelState.IsValid)
            {
                empl.Updated(employee, empl);              
                Upload(file, file1, empl);

                if (skills!=null)
                {
                    foreach (var item in skills)
                    {
                        skils.Add(db.skilss.Single(x => x.ID == item));
                        foreach (var tag in skils)
                        {
                            if (!employee.skils.Contains(tag))
                            {
                                employee.skils.Add(tag);
                                tag.Employees.Add(employee);
                            }

                        }   }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddEmployee", viewmodel);
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
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList()

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
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var employee = db.emps.Find(id);
            db.emps.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
        public ActionResult Index(bool? status, int? id)
        {
            List<Employee> emps = new List<Employee>();
            if (status.HasValue)
            {
                emps = db.emps.Include(c => c.job).Where(x => x.EmploymentStatus == status).ToList();
            }
            else
            {
                emps = db.emps.Include(c => c.job).ToList();
            }
            var viewmodel = new EmployeeViewModel()
            {
                emps = emps,
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList(),
            };
            if (id.HasValue)
            {
                List<int> ids = db.emps.Select(x => x.ID).ToList();
                if (ids.Contains(id.Value))
                {
                    viewmodel.employee = db.emps.Single(x => x.ID == id);
                }
            }
            else
            {
              
            }

            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult IndexPost(int? pos, int? skil)
        {
            List<Employee> emps = new List<Employee>();
            if (pos.HasValue)
            {
                emps = db.emps.Where(x => x.JobPositionID == pos).ToList();
            }
            else if (skil.HasValue)
            {
                var skills = db.skilss.Where(x => x.ID == skil).ToList();
                var e = db.emps.Include(x => x.skils).ToList();
                foreach (var item in skills)
                {
                    emps = e.Where(x => x.skils.Contains(item)).ToList();
                }
            }
            if (pos.HasValue && skil.HasValue)
            {
                var skills = db.skilss.Where(x => x.ID == skil).ToList();
                var e = db.emps.Include(x => x.skils).ToList();
                foreach (var item in skills)
                {
                    emps = e.Where(x => x.JobPositionID == pos && x.skils.Contains(item)).ToList();
                }
            }
            else if ((!pos.HasValue||pos.Value==0) && (!skil.HasValue ||skil.Value==0))
            {
                ModelState.AddModelError("", "You must choose a search parameter");
            }

            var viewmodel = new EmployeeViewModel()
            {
                emps = emps,
                positions = db.positions.Where(x => x.Status == true).ToList(),
                skils = db.skilss.ToList()
            };
           
            return View("Index", viewmodel);
        }
        public void Upload(HttpPostedFileBase file, HttpPostedFileBase file1, Employee employee)
        {
            if (employee.CV==null)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var name = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Managment/CV'S"), name);
                    file.SaveAs(path);
                    employee.CV = "~/Managment/CV'S/" + file.FileName;
                }
                else
                {
                    employee.CV = "No CV uploaded";
                } 
            }
            if (employee.Photo==null)
            {
                if (file1 != null && file1.ContentLength > 0)
                {
                    var name1 = Path.GetFileName(file1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/Managment/Photos"), name1);
                    file1.SaveAs(path1);
                    employee.Photo = "~/Managment/Photos/" + file1.FileName;
                }
                else
                {
                    employee.Photo = "No photo uploaded";
                } 
            }
        }
    }

}