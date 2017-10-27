using HRiVote.DAL;
using HRiVote.Models;
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
        public ActionResult Index()
        {

            return View(db.positions.Where(x=>x.Status==true).ToList());
        }
        public ActionResult Edit(int id)
        {
            var position = db.positions.Single(x => x.ID == id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobPosition possition)
        {
            if (ModelState.IsValid)
            {
                var postion = db.positions.Single(x => x.ID == possition.ID);
               
                postion.Title = possition.Title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possition);
            
        }
        
    }
}