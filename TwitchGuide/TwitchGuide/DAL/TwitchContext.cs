using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using TwitchGuide.Models;

namespace TwitchGuide.DAL
{
    public class TwitchContext : DbContext
    {
        public TwitchContext() : base("name=AzureConnection") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Timeblock> Timeblocks { get; set; }
    }
}