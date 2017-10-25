using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Filters
{
    public class MeetingFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            meet();
        }

        public void meet()
        {
            using (Entity db =new Entity())
            {
                var meets = db.sredbi.Where(x => x.MeetingDay.Day - DateTime.Now.Day <= -5).ToList();
                db.sredbi.RemoveRange(meets);
                db.SaveChanges(); 
            }
        }
    }
}