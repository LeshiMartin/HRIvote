using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRiVote.Controllers.API
{
    public class FileController : ApiController
    {
        [HttpPost]
        public void CreateEntry(string Name)
        {
            if (!Directory.Exists(Name))
            {
                Directory.CreateDirectory(Name);
            }
        }
    }
}
