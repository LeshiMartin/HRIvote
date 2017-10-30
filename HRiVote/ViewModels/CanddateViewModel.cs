using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.ViewModels
{
    public class CanddateViewModel
    {
        public IEnumerable<Candidate> Candidate { get; set; }
        public Candidate candidate { get; set; }
    }
}