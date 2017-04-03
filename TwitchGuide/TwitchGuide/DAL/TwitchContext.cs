namespace TwitchGuide.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class TwitchContext : DbContext
    {
        public TwitchContext()
            : base("name=AzureConnection")
            //: base("name=DefaultConnection")
        {
            //Disable Code-First Migrations
            Database.SetInitializer<TwitchContext>(null);
        }

        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Timeblock> Timeblocks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Schedules)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(true);
        }
    }
}
