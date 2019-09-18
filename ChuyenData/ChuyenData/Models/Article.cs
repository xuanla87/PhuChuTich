namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            BrandComments = new HashSet<BrandComment>();
            SitePagesArticles = new HashSet<SitePagesArticle>();
        }

        public int ArticleID { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(500)]
        public string ThumbnailTitle { get; set; }

        [Required]
        [StringLength(1000)]
        public string Subject { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public int ArticleTypeID { get; set; }

        public int NumberOfWord { get; set; }

        public int NumberOfMedia { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        [Required]
        [StringLength(7)]
        public string TargetUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        [StringLength(100)]
        public string Source { get; set; }

        public bool HomePage { get; set; }

        public bool Featured { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ApprovedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateUser { get; set; }

        public int Price { get; set; }

        public int ImagePrice { get; set; }

        public int Views { get; set; }

        public virtual ArticleType ArticleType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandComment> BrandComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePagesArticle> SitePagesArticles { get; set; }
    }
}
