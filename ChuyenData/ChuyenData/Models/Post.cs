namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("News.Posts")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostAttributes = new HashSet<PostAttribute>();
            RelatedPosts = new HashSet<RelatedPost>();
            Tags = new HashSet<Tag>();
            SitePages = new HashSet<SitePage>();
            Topics = new HashSet<Topic>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string SubTitle { get; set; }

        [Required]
        [StringLength(300)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(300)]
        public string LargeThumbnail { get; set; }

        [Required]
        [StringLength(500)]
        public string ThumbnailTitle { get; set; }

        public int PostTypeId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Subject { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required]
        [StringLength(50)]
        public string TargetUrl { get; set; }

        public int AuthorId { get; set; }

        public int SourceId { get; set; }

        public bool Visible { get; set; }

        public bool HomePage { get; set; }

        public bool Featured { get; set; }

        public bool AllowComment { get; set; }

        public int Views { get; set; }

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

        public virtual Author Author { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostAttribute> PostAttributes { get; set; }

        public virtual PostType PostType { get; set; }

        public virtual Sources1 Sources1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelatedPost> RelatedPosts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePage> SitePages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
