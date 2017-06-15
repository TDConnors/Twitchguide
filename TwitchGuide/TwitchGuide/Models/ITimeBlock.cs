namespace TwitchGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public interface ITimeBlock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int Index { get; set; }

        [Range(1, 24)]
        [Key]
        [Column(Order = 0)]
        byte StartHour { get; set; }

        [Range(0, 59)]
        [Key]
        [Column(Order = 1)]
        byte StartMinute { get; set; }

        [Range(1, 24)]
        [Key]
        [Column(Order = 2)]
        byte EndHour { get; set; }

        [Range(0, 59)]
        [Key]
        [Column(Order = 3)]
        byte EndMinute { get; set; }

        [Range(1, 7)]
        [Key]
        [Column(Order = 4)]
        byte Day { get; set; }
    }
}