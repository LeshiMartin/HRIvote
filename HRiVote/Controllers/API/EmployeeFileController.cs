using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class EmployeeFileController : ApiController
    {
        Entity db;
        public EmployeeFileController()
        {
            db = new Entity();
        }
        [HttpDelete]
        public void DeleteFile(int id)
        {
            var empl = db.empf.Single(x => x.Id == id);
            if (empl == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {

                db.empf.Remove(empl);
                // emp.IsAvailable = true;
                db.SaveChanges();

            }
        }
    }
}
