namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticlePageSetting
    {
        public int ArticlePageSettingID { get; set; }

        public int SitePageID { get; set; }

        public int PageSize { get; set; }

        [Required]
        [StringLength(1000)]
        public string HeaderTemplate { get; set; }

        [Required]
        [StringLength(1000)]
        public string FooterTemplate { get; set; }

        [Required]
        [StringLength(1000)]
        public string ItemTemplate { get; set; }

        [Required]
        [StringLength(1000)]
        public string AltItemTemplate { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
