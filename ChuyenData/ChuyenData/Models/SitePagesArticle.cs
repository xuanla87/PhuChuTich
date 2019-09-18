namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SitePagesArticle
    {
        [Key]
        public int SitePagesArticlesID { get; set; }

        public int SitePageID { get; set; }

        public int ArticleID { get; set; }

        public bool Visible { get; set; }

        [Required]
        [StringLength(20)]
        public string ApprovedUser { get; set; }

        public virtual Article Article { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
