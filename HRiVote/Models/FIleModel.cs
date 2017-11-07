using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace HRiVote.Models
{
    public class FIleModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public ICollection<string> Images { get; set; }
        public FIleModel()
        {
            Images = new Collection<string>();
        }
    }
}