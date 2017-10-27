using System;
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
        public bool Status { get; set; }
<<<<<<< HEAD
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd,MM,yyyy")]
=======
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:dd,MM,yyyy")]
>>>>>>> 971db4c0b7ac89e00dc1ad772abd669c8da598e9
        public DateTime? StartOfJobOpenning { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd,MM,yyyy")]
        public DateTime? EndOfJobOpenning { get; set; }
    }
}