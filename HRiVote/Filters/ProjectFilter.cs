using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRiVote.Filters
{
    public class ProjectFilter:ActionFilterAttribute
    {


        public void Project()
        {
            using (Entity db = new Entity())
            {
                var projects = db.project.ToList();
                foreach(var item in projects)
                {
                    if (item.EndDate.Value.Day - DateTime.Now.Day < -2)
                    {
                        item.Status = false;
                    }
                  
                }
                var plist = db.project.Where(x => x.EndDate.Value.Day - DateTime.Now.Day < -4 && x.Status == false).ToList();
                db.project.RemoveRange(plist);
                db.SaveChanges();
            }
        }
    }
}