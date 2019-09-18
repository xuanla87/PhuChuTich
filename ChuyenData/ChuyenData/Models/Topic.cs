namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("News.Topics")]
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            Posts = new HashSet<Post>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(300)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(300)]
        public string LargeThumbnail { get; set; }

        public bool HomePage { get; set; }

        public bool Featured { get; set; }

        public bool Hot { get; set; }

        public bool Visible { get; set; }

        public DateTime ApprovedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ApprovedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedByUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
