using HRiVote.DAL;
using HRiVote.Models;
using HRiVote.ViewModels;
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
        public ActionResult Index(bool? status, int? id, int? Editid)
        {
            List<OpenPosition> open = new List<OpenPosition>();
            if (status.HasValue)
            {
                open = db.pozicii.Where(x => x.Status == status).ToList();

            }
            else
            {
                open = db.pozicii.ToList();
            }
            var ViewModel = new OpenJobViewModel
            {
                opens = open,
            };
            List<int> ids = db.pozicii.Select(x => x.ID).ToList();
            if (id.HasValue)
            {
                if (ids.Contains(id.Value))
                {
                    ViewModel.open = ViewModel.opens.Single(x => x.ID == id); 
                }
            }
            if (Editid.HasValue)
            {
                ViewModel.Edit = ViewModel.opens.Where(x => x.ID == Editid).ToList();
            }

            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost(OpenPosition item)
        {
            var job = db.pozicii.Single(x => x.ID == item.ID);
            if(job == null)
            {
                return HttpNotFound();
            }
            else
            {
                //item.Update(item, job);
                job.Description = item.Description;
                job.EndOfJobOpenning = item.EndOfJobOpenning;
                job.Name = item.Name;
                job.StartOfJobOpenning = item.StartOfJobOpenning;
                job.Status = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult OpenPosition()
        {
            Models.OpenPosition open = new Models.OpenPosition();
            return View(open);

        }
        //public ActionResult UpdatePossition(int id)
        //{
        //    var possition = db.pozicii.Single(x => x.ID == id);
        //    return View("OpenPosition", possition);
        //}
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
                    return View("OpenPosition", open);
                }
            }
            else
            {
                var pos = db.pozicii.Single(x => x.ID == open.ID);
                pos.Update(open, pos);
                pos.Status = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Details(int id)
        //{
        //    var pos = db.pozicii.Find(id);
        //    if (pos == null)
        //    {
        //        return HttpNotFound();

        //    }
        //    return View(pos);
        //}
    }
}