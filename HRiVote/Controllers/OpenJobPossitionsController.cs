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
            var possitions = db.positions.ToList();
            var job = db.pozicii.Single(x => x.ID == item.ID);
            if(job == null)
            {
                return HttpNotFound();
            }
            else
            {
                //UPdatte na oglasot i dokolku taa pozicija ja nema vo bazata na rabotni pozicii se dodava
                job.Update(job, item);               
                job.Status = true;
                if (!possitions.Select(x => x.Title).Contains(item.Name))
                {
                    AddtoPossitions(item);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult OpenPosition()
        {
            Models.OpenPosition open = new Models.OpenPosition();
            return View(open);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OpenPosition open)
        {
            var possitions = db.positions.ToList();
            if (open.ID == 0)
            {

                if (ModelState.IsValid)
                {
                    open.Status = true;
                    db.pozicii.Add(open);
                    if (!possitions.Select(x => x.Title).Contains(open.Name))
                    {
                        AddtoPossitions(open);
                    }
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
                if (!possitions.Select(x => x.Title).Contains(open.Name))
                {
                    AddtoPossitions(open);                   
                }
                pos.Update(open, pos);
                pos.Status = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //Metod za Dodavanje na pozicii vo clasata na pozicii
            public void AddtoPossitions(OpenPosition open)
            {
            var possition = new JobPosition()
            {
                Status = true,
                Title = open.Name
            };
            db.positions.Add(possition);
            }
    }
}