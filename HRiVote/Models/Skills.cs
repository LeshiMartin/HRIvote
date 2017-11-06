﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Skills
    {
        public int ID { get; set; }
        public string Skill { get; set; }
        public bool status { get; set; }
       public virtual ICollection<Employee> Employees { get;private set; }
        public Skills()
        {
            Employees = new Collection<Employee>();
        }
    }
}