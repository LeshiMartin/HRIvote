using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRiVote.DAL;
using HRiVote.Models;
using System.IO;

namespace HRiVote.Controllers
{
    [HandleError(View = "DetailedError")]
    public class CandidateController : Controller
    {
        private Entity db = new Entity();

        // GET: Candidate
        public ActionResult Index(string currentFilter,string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            var candidates = from cand in db.aplikanti
                            select cand;
            if(!String.IsNullOrEmpty(searchString))
            {
                candidates = candidates.Where(c => c.FirstName.Contains(searchString) || c.LastName.Contains(searchString));
                return View(candidates.ToList());
            }
            else
            {
                return View(db.aplikanti.ToList());
            }
        }

        // GET: Candidate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.aplikanti.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Email,PhoneNumber,CV,InterviewDate,InterviewTime")] Candidate candidate, HttpPostedFileBase file,DateTime? Time, int? InterviewRound)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    file.SaveAs(_path);
                    candidate.CV = "~/Uploads/" + file.FileName;
                }
                else
                {
                    candidate.CV = "Cv not Uploaded";
                    //ViewBag.Message = "Cv not Uploaded";
                }
                if (Time.HasValue)
                {
                    candidate.InterviewTime = Time.Value;
                }
                else
                {
                    ModelState.AddModelError("Time", "This field is mandatory");
                }
                if(candidate.InterviewDate < DateTime.Now)
                {
                    ModelState.AddModelError("InterviewDate", "Vnesovte minat datum.");
                    return View(candidate);
                }
                if(InterviewRound.HasValue)
                {
                    candidate.InterviewRound = InterviewRound.Value;
                }
                db.aplikanti.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index","Candidate");
            }

            return View(candidate);
        }

        // GET: Candidate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.aplikanti.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Email,PhoneNumber,InterviewDate,InterviewTime, InterviewRound")] Candidate candidate, DateTime? Time,HttpPostedFileBase file, int? InterviewRound, DateTime? Date)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    file.SaveAs(_path);
                    candidate.CV = "~/Uploads/" + file.FileName;
                }
                else
                {
                    candidate.CV = "Cv not Uploaded";
                    //ViewBag.Message = "Cv not Uploaded";
                }
                
                db.Entry(candidate).State = EntityState.Modified;
                if(Time.HasValue)
                {
                    candidate.InterviewTime = Time.Value;
                }
                if(Date.HasValue)
                {
                    candidate.InterviewDate = Date.Value;
                }
                if(InterviewRound.HasValue)
                {
                    candidate.InterviewRound = InterviewRound.Value;
                }
                //candidate.InterviewTime = Time.Value;
                //candidate.InterviewDate = Date.Value;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.aplikanti.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.aplikanti.Find(id);
            db.aplikanti.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
