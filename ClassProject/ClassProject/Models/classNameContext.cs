namespace ClassProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class classNameContext : DbContext
    {
        public classNameContext()
            : base("name=classNameContext")
        {
        }

        public virtual DbSet<Name> Names { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
