using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassProject.Models
{
    public class Story
    {
        public int StoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Headline { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public int Likes { get; set; }

        public Story()
        {
            Likes = 0;
        }
    }
}