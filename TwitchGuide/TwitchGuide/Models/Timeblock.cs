namespace TwitchGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Timeblock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Index { get; set; }

        [Key]
        [Column(Order = 0)]
        public byte StartHour { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte StartMinute { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte EndHour { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte EndMinute { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte Day { get; set; }
    }
}
