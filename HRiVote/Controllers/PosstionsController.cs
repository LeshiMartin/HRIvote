using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

using System.Web.Mvc;

namespace HRiVote.Controllers
{
    public class PosstionsController : Controller
    {
        private Entity db = new Entity();
        // GET: Posstions
        public ActionResult Index(bool? status,bool? skilStatus)
        {
            var skills = db.skilss.ToList();
            var possitions = db.positions.ToList();
            if (!status.HasValue)
            {
                possitions = db.positions.Where(x => x.Status == true).ToList();
            }
            else
            {
                possitions = db.positions.Where(c => c.Status == status).ToList();
            }
            if (!skilStatus.HasValue)
            {
                skills = db.skilss.Where(s => s.status == true).ToList();
            }
            else
            {
                skills = db.skilss.Where(s => s.status == skilStatus).ToList();
            }
            var viewmodel = new PositionSkillViewModel()
            {
                poss = possitions,
                skils = skills
            };
            return View(viewmodel);
        }
        public ActionResult AddPosition(int? id,int?skillid)
        {
            var viewmodel = new PosstionSkilllViewModel()
            {
                jobPosition = new JobPosition(),
                skills = new Skills()
            };
            if (id.HasValue)
            {
                TempData["value"] = id;
                TempData["skill"] = skillid;
            }
            if (skillid.HasValue) { }
            return View(viewmodel);
        }
        //public ActionResult Create(int?id)
        //{
        //    var viewmodel = new PosstionSkilllViewModel()
        //    {
        //        jobPosition = new JobPosition(),
        //        skills = new Skills()
        //    };
        //    if (id.HasValue)
        //    {
        //        TempData["skill"] = id;
        //    }
        //    return View("AddPosition",viewmodel);
        //}
        [HttpPost]
        [ActionName("AddPosition")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(JobPosition jobPosition, string skill)
        {
            if (ModelState.IsValid)
            {
                if (jobPosition.Title!=null)
                {
                    if (!db.positions.Select(x => x.Title).Contains(jobPosition.Title))
                    {
                        jobPosition.Status = true;
                        db.positions.Add(jobPosition);
                        db.SaveChanges();
                    } 
                }
                if (!string.IsNullOrWhiteSpace(skill))
                {
                    if (!db.skilss.Select(x => x.Skill).Contains(skill))
                    {
                        var skils = new Skills() { status = true, Skill = skill };
                        db.skilss.Add(skils);
                        db.SaveChanges();

                    } 
                }
                var h = (int?)TempData["value"];
                var k = (int?)TempData["skill"];
                if (h.HasValue)
                {
                    return RedirectToAction("AddEmployee","Employee");
                }
                else if (k.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(jobPosition);
        }

    }
}