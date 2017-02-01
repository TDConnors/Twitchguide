namespace ClassProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Name
    {
        public int NameID { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(50)]
        public string Names { get; set; }
    }
}
