using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    

    public class SkillController : ApiController
    {
        private Entity db;
        public SkillController()
        {
            db = new Entity();
        }
        [HttpDelete]
        public void Remove(int? id)
        {
            var skill = db.skilss.FirstOrDefault(x => x.ID == id);
            if (skill == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            else
            {
                skill.status = false;
                db.SaveChanges();
            }
        }
        [HttpPut]
        public void Add(int? id)
        {
            var skill = db.skilss.Single(x => x.ID == id);
            if (skill == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            else
            {
                skill.status =true;
                db.SaveChanges();
            }
        }
    }
}
