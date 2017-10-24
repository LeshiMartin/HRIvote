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
    
    public class MeetingController : ApiController
    {
        private Entity db = new Entity();
        public IEnumerable<Meeting> GetMeeting()
        {
            return db.sredbi.ToList();
        }

        public Meeting GetMeeting(int id)
        {
            var meeting = db.sredbi.SingleOrDefault(x => x.ID == id);
            if (meeting == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return meeting;
        }
        [HttpPost]
        public void CreateMeeting ([FromBody]Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.sredbi.Add(meeting);
                db.SaveChanges();
                throw new HttpResponseException(HttpStatusCode.OK);

            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        [HttpPut]
        public void UpdateMeeting ([FromBody]Meeting meeting,int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var meet = db.sredbi.Single(x => x.ID == id);
            if (meet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            meet.MeetingDay = meeting.MeetingDay;
            meet.MeetingTime = meeting.MeetingTime;
            db.SaveChanges();
            throw new HttpResponseException(HttpStatusCode.OK);
        }
        [HttpDelete]
        public void DeleteMeeting(int id)
        {
            var meeting = db.sredbi.FirstOrDefault(x => x.ID == id);
            db.sredbi.Remove(meeting);
            db.SaveChanges();
            throw new HttpResponseException(HttpStatusCode.OK);
        }
    }
}
