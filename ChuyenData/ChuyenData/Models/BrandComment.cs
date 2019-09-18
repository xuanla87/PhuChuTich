namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BrandComment
    {
        public int ID { get; set; }

        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime CommentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public bool Visible { get; set; }

        public virtual Article Article { get; set; }
    }
}
