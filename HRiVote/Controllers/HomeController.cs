using HRiVote.DAL;
using HRiVote.Filters;
using HRiVote.Models;
using HRiVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Controllers
{
    [HandleError(View = "DetailedError")]
    public class HomeController : Controller
    {
        private Entity db = new Entity();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Calendar
        [MeetingFilter]
        [ProjectFilter]
        public ActionResult Index()
        {
            //var meets = db.sredbi.Where(x => x.MeetingDay.Day - DateTime.Now.Day <= -5).ToList();
            //db.sredbi.RemoveRange(meets);
            //db.SaveChanges();                    
            return View();
        }
        
    }
}