namespace TwitchGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Timeblock : ITimeBlock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Index { get; set; }

        [Range(1, 24)]
        [Key]
        [Column(Order = 0)]
        public byte StartHour { get; set; }

        [Range(0, 59)]
        [Key]
        [Column(Order = 1)]
        public byte StartMinute { get; set; }

        [Range(1, 24)]
        [Key]
        [Column(Order = 2)]
        public byte EndHour { get; set; }

        [Range(0, 59)]
        [Key]
        [Column(Order = 3)]
        public byte EndMinute { get; set; }

        [Range(1, 7)]
        [Key]
        [Column(Order = 4)]
        public byte Day { get; set; }
    }
}
