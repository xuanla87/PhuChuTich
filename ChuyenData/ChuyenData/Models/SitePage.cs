namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SitePage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SitePage()
        {
            ArticlePageSettings = new HashSet<ArticlePageSetting>();
            ContentPages = new HashSet<ContentPage>();
            Events = new HashSet<Event>();
            ExternSitePageProperties = new HashSet<ExternSitePageProperty>();
            HomepageArticleModulePages = new HashSet<HomepageArticleModulePage>();
            ModuleArticlePages = new HashSet<ModuleArticlePage>();
            SitePagesArticles = new HashSet<SitePagesArticle>();
            SitePageSettings = new HashSet<SitePageSetting>();
            UserArticlePages = new HashSet<UserArticlePage>();
            Posts = new HashSet<Post>();
        }

        public int SitePageID { get; set; }

        public int ParentPageID { get; set; }

        [Required]
        [StringLength(100)]
        public string PageName { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int PageLevel { get; set; }

        [Required]
        [StringLength(255)]
        public string PagePath { get; set; }

        public int SortOrder { get; set; }

        public bool ShowInMenu { get; set; }

        public bool ShowInMenuLeft { get; set; }

        public bool ShowInMenuBottom { get; set; }

        public int PageType { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required]
        [StringLength(7)]
        public string TargetUrl { get; set; }

        public bool Visible { get; set; }

        [Required]
        [StringLength(255)]
        public string Icon { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string AdsContent { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(1000)]
        public string Keyword { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlePageSetting> ArticlePageSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContentPage> ContentPages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExternSitePageProperty> ExternSitePageProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomepageArticleModulePage> HomepageArticleModulePages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleArticlePage> ModuleArticlePages { get; set; }

        public virtual SitePageType SitePageType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePagesArticle> SitePagesArticles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SitePageSetting> SitePageSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserArticlePage> UserArticlePages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
