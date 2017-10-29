using HRiVote.DAL;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class PossitionsViewModel
    {
        public IEnumerable<JobPosition> possitions
        {
            get
            {
                using (Entity db = new Entity())
                {
                    return db.positions.ToList();
                }
            }
        }
    }
}