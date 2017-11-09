using HRiVote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{

    public class TreeController : ApiController
    {
        public Entity db;
        public TreeController()
        {
            db = new Entity();
        }
     
    }
}
