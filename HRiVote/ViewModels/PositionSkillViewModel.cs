using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRiVote.Models;

namespace HRiVote.ViewModels
{
    public class PositionSkillViewModel
    {
        public IEnumerable<JobPosition> poss { get; set; }
        public IEnumerable<Skills> skils { get; set; }
    }
}