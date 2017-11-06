using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class CalendarController : ApiController
    {
        private Entity db;
        public CalendarController()
        {
            db = new Entity();
        }
        [HttpDelete]
        public void Remove(int id)
        {
            var cal = db.kalendar.Single(x => x.Id == id);
            if (cal == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var emp = db.emps.Single(x => x.ID == cal.EmployeeID);
                cal.Remove();
               // emp.IsAvailable = true;
                db.SaveChanges();
                //db.kalendar.Remove()
            }
        }
    }
}
