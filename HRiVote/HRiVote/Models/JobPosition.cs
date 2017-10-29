using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class JobPosition
    {
        public int ID { get; set; }
        public string Title { get; set; }
        
        [ScaffoldColumn(false)]
        public bool Status { get; set; }

    }
}