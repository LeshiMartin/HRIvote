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
        public ActionResult Index(bool? status)
        {
            var possitions = db.positions.ToList();
            if (!status.HasValue)
            {
                possitions = db.positions.Where(x => x.Status == true).ToList();
            }
            else
            {
                possitions = db.positions.Where(c => c.Status == status).ToList();
            }
            return View(possitions);
        }
        public ActionResult AddPosition(int? id)
        {
            var viewmodel = new PosstionSkilllViewModel()
            {
                jobPosition = new JobPosition(),
                skills = new Skills()
            };
            if (id.HasValue)
            {
                TempData["value"] = id;
            }
            return View(viewmodel);
        }
        [HttpPost]
        [ActionName("AddPosition")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(JobPosition jobPosition, string skill)
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
                    var skils = new Skills() { status = true, Skill = skill };
                    db.skilss.Add(skils);
                    db.SaveChanges();

                }
                var h = (int?)TempData["value"];
                if (h.HasValue)
                {
                    return RedirectToAction("AddEmployee","Employee");
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