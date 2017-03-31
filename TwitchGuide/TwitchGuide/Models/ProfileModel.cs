using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitchGuide.Models
{
    public class ProfileModel
    {
        public List<Timeblock> TimeblockObj { get; set; }
        public User UserObj { get; set; }
    }
}