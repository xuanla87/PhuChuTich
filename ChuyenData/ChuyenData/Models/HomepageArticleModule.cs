namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HomepageArticleModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HomepageArticleModule()
        {
            HomepageArticleModulePages = new HashSet<HomepageArticleModulePage>();
        }

        public int HomepageArticleModuleID { get; set; }

        public int ModuleID { get; set; }

        public int NumberOfFeaturedArticle { get; set; }

        public int NumberOfHomepageArticle { get; set; }

        [Required]
        [StringLength(1200)]
        public string FeaturedArticleHeaderTemplate { get; set; }

        [Required]
        [StringLength(1200)]
        public string FeaturedArticleFooterTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string FeaturedArticleItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string FeaturedArticleAltItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string HomepageArticleHeaderTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string HomepageArticleFooterTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string HomepageArticleItemTemplate { get; set; }

        [Required]
        [StringLength(700)]
        public string HomepageArticleAltItemTemplate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomepageArticleModulePage> HomepageArticleModulePages { get; set; }

        public virtual Module Module { get; set; }
    }
}
