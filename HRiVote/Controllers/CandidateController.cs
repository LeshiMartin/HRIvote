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
               // viewmodel.Candidate = viewmodel.Candidate.Except(viewmodel.cans);
                TempData["id"] = updateid.Value;
            }

                return View(viewmodel);           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Index")]
        public ActionResult IndexPost(HttpPostedFileBase file,Candidate item)
        {
            //int ID = (int)TempData["id"];
            //TempData.Keep("id");
            //var aplikant = db.aplikanti.Single(x => x.ID == ID);
            var viewmodl = new CanddateViewModel()
            {
                Candidate = db.aplikanti.ToList()
            };
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Managment/Document's"), _FileName);
                    file.SaveAs(_path);
                    item.CV = "~/Managment/Document's" + file.FileName;
                }
                else
                {
                    item.CV = "Cv not Uploaded";
                    //ViewBag.Message = "Cv not Uploaded";
                }
                var candidate = db.aplikanti.Single(x => x.ID == item.ID);
                candidate.FirstName = item.FirstName;
                candidate.LastName = item.LastName;
                candidate.Email = item.Email;
                candidate.InterviewDate = item.InterviewDate;
                candidate.InterviewRound = item.InterviewRound;
                candidate.InterviewTime = item.InterviewTime;
                candidate.PhoneNumber = item.PhoneNumber;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index",viewmodl);
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
                    string _path = Path.Combine(Server.MapPath("~/Managment/Document's"), _FileName);
                    file.SaveAs(_path);
                    candidate.CV = "~/Managment/Document's" + file.FileName;
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
