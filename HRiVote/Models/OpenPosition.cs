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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool Status { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime StartOfJobOpenning { get; set; }
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime EndOfJobOpenning { get; set; }
        public OpenPosition()
        {

        }
        public void Update(OpenPosition old,OpenPosition newpos)
        {
            old.Description = newpos.Description;
            old.EndOfJobOpenning = newpos.EndOfJobOpenning;
            old.Name = newpos.Name;
            old.StartOfJobOpenning = newpos.StartOfJobOpenning;
        }
    }
}