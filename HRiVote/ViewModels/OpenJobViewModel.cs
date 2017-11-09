using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRiVote.Models;

namespace HRiVote.ViewModels
{
    public class OpenJobViewModel
    {
        public IEnumerable<OpenPosition> opens { get; set; }
        public OpenPosition open { get; set; }
        public IEnumerable<OpenPosition> Edit { get; set; }
    }
}