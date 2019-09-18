namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Video
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Video()
        {
            Comments = new HashSet<Comment>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Source { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(300)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(300)]
        public string LargeThumbnail { get; set; }

        [Required]
        [StringLength(300)]
        public string VideoUrl { get; set; }

        [Required]
        [StringLength(300)]
        public string YouTubeVideoUrl { get; set; }

        public int Views { get; set; }

        public bool Visible { get; set; }

        public bool HomePage { get; set; }

        public DateTime ApprovedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateByUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
