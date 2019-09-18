namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feedback
    {
        public int FeedbackID { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        public DateTime FeedbackDate { get; set; }
    }
}
