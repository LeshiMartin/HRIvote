﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class OpenPosition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool Status { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
         public DateTime? StartOfJobOpenning { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndOfJobOpenning { get; set; }
    }
}