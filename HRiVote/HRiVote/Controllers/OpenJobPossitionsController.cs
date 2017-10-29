using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Controllers
{
    public class OpenJobPossitionsController : Controller
    {
        private Entity db = new Entity();
        // GET: OpenJobPossitions
        public ActionResult Index(bool? status)
        {
           List< OpenPosition> open = new List<OpenPosition>();
            if (status.HasValue)
            {
                open = db.pozicii.Where(x => x.Status == status).ToList();

            }
            else
            {
                open = db.pozicii.ToList();
            }
            return View(open);
        }
        public ActionResult OpenPosition()
        {
            Models.OpenPosition open = new Models.OpenPosition();
            return View(open);

        }
        public ActionResult UpdatePossition(int id)
        {
            var possition = db.pozicii.Single(x => x.ID == id);
            return View("OpenPosition", possition);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OpenPosition open)
        {
            if (open.ID == 0)
            {
                
                if (ModelState.IsValid)
                {
                    open.Status = true;
                    db.pozicii.Add(open);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "We couldn't validate your input please try again");
                    return View(open);
                }
            }
            else
            {
                var pos = db.pozicii.Single(x => x.ID == open.ID);
                pos.Name = open.Name;
                pos.StartOfJobOpenning = open.StartOfJobOpenning;
                pos.Description = open.Description;
                pos.EndOfJobOpenning = open.EndOfJobOpenning;
                pos.Status = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var pos = db.pozicii.Find(id);
            if (pos == null)
            {
                return HttpNotFound();

            }
            return View(pos);
        }
    }
}