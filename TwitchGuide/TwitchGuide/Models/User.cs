using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitchGuide.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Handle { get; set; }
        public virtual ICollection<Timeblock> Timeblocks { get; set; } //this is an array of Timeblocks, hopefully

    }
}