namespace ClassProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsContext : DbContext
    {
        public NewsContext()
            : base("name=NewsContext")
        {
        }

        public virtual DbSet<Story> Stories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
