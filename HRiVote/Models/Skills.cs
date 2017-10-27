using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class Skills
    {
        public int ID { get; set; }
        public string Skill { get; set; }
<<<<<<< HEAD
       public bool Status { get; set; }
        public ICollection<Employee> Employees { get; set; }
=======
        public bool status { get; set; }
       public virtual ICollection<Employee> Employees { get; set; }
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
    }
}