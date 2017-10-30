﻿using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class CandidateController : ApiController
    {
        Entity db = new Entity();
        [HttpDelete]
        public void DeleteCandidate(int id)
        {
            var candidate = db.aplikanti.FirstOrDefault(x => x.ID == id);
           
            db.aplikanti.Remove(candidate);
            db.SaveChanges();
        }
        [HttpPut]
        public void UpdateInterviewDate([FromBody] Candidate candidate,int id)
        {
            var aplikant = db.aplikanti.Single(x => x.ID == id);
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if (aplikant == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                aplikant.InterviewDate = candidate.InterviewDate;
                aplikant.InterviewTime = candidate.InterviewTime;
                db.SaveChanges();
                throw new HttpResponseException(HttpStatusCode.OK);
            }
        }
    }
}
