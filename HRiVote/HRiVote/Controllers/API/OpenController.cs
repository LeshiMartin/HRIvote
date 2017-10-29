using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class OpenController : ApiController
    {
        private Entity db = new Entity();
        [HttpDelete]
        public void DeleteOpen(int id)
        {
            var job = db.pozicii.FirstOrDefault(x => x.ID == id);
            if (job == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                job.Status = false;
                db.SaveChanges();
            }
        }
        [HttpPut]
        public void update(int id)
        {
            var open = db.pozicii.Single(x => x.ID == id);
            if (open == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                open.Status = true;
                db.SaveChanges();
            }
        }
    }
}
