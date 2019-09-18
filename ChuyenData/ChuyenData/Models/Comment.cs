namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("News.Comments")]
    public partial class Comment
    {
        public long Id { get; set; }

        public long ContentId { get; set; }

        [Required]
        [StringLength(12)]
        public string ContentType { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime CommentDate { get; set; }

        [Required]
        public string Content { get; set; }

        public bool HomePage { get; set; }

        public bool Visible { get; set; }

        public virtual Video Video { get; set; }

        public virtual Post Post { get; set; }
    }
}
