using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class PossitonsController : ApiController
    {
        private Entity db = new Entity();
        public IEnumerable<JobPosition> GETPossitions()
        {
            return db.positions.ToList();
        }
        public JobPosition GETJob(int id)
        {
            var poss = db.positions.Single(x => x.ID == id);
            if (poss == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return (poss);
        }
        [HttpPost]
        public void CreateJob([FromBody] JobPosition position)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                db.positions.Add(position);
                db.SaveChanges();
            }
        }
        [HttpDelete]
        public void DeletePosition(int id)
        {
            var pos = db.positions.FirstOrDefault(x => x.ID == id);
            if (pos == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                pos.Status = false;
                db.SaveChanges();
            }
        }
    }
}
