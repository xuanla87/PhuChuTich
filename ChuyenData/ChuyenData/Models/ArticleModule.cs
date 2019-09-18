namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticleModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArticleModule()
        {
            ModuleArticlePages = new HashSet<ModuleArticlePage>();
        }

        public int ArticleModuleID { get; set; }

        public int ModuleID { get; set; }

        public int NumberOfTopArticle { get; set; }

        public int NumberOfMoreArticle { get; set; }

        public int HomePageArticle { get; set; }

        public int FeaturedArticle { get; set; }

        public bool MostView { get; set; }

        public bool MostComment { get; set; }

        [Required]
        [StringLength(1200)]
        public string TopArticleHeaderTemplate { get; set; }

        [Required]
        [StringLength(1200)]
        public string TopArticleFooterTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string TopArticleItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string TopArticleAltItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string MoreArticleHeaderTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string MoreArticleFooterTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string MoreArticleItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string MoreArticleAltItemTemplate { get; set; }

        public virtual Module Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleArticlePage> ModuleArticlePages { get; set; }
    }
}
