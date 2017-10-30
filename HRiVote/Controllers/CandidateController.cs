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
using HRiVote.ViewModels;

namespace HRiVote.Controllers
{
    [HandleError(View = "DetailedError")]
    public class CandidateController : Controller
    {
        private Entity db = new Entity();

        // GET: Candidate
        public ActionResult Index(int? id,int? candidateid,int? updateid)
        {
            List<Candidate> Can = db.aplikanti.ToList();
            List<Candidate> candidates = new List<Candidate>();
            if (id > 0)
            {
                candidates = Can.Where(x => x.InterviewRound == id).ToList();
            }
            else
            {
                candidates = Can;
            }
        
            
            var viewmodel = new CanddateViewModel()
            {
                Candidate = candidates
            };

            if (candidateid.HasValue)
            {
                viewmodel.candidate = viewmodel.Candidate.Single(x => x.ID == candidateid);
            }
            if (updateid.HasValue)
            {
                viewmodel.cans = viewmodel.Candidate.Where(x => x.ID == updateid).ToList();
            }

                return View(viewmodel);
            
        }
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
         [ValidateAntiForgeryToken]
        public ActionResult Create( Candidate candidate, HttpPostedFileBase file, int? InterviewRound)
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
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Email,PhoneNumber,InterviewDate,InterviewTime, InterviewRound")] Candidate candidate, HttpPostedFileBase file, int? InterviewRound)
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
