namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Module()
        {
            ListModuleItems = new HashSet<ListModuleItem>();
            ArticleModules = new HashSet<ArticleModule>();
            HomepageArticleModules = new HashSet<HomepageArticleModule>();
            HtmlModules = new HashSet<HtmlModule>();
            ImageModules = new HashSet<ImageModule>();
            ModuleSettings = new HashSet<ModuleSetting>();
        }

        public int ModuleID { get; set; }

        public int SiteZoneID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int ModuleType { get; set; }

        public int SortOrder { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedUser { get; set; }

        public bool Visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListModuleItem> ListModuleItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleModule> ArticleModules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomepageArticleModule> HomepageArticleModules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HtmlModule> HtmlModules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageModule> ImageModules { get; set; }

        public virtual ModuleControl ModuleControl { get; set; }

        public virtual SiteZone SiteZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleSetting> ModuleSettings { get; set; }
    }
}
