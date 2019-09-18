namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserArticlePage
    {
        public int UserArticlePageID { get; set; }

        public int UserID { get; set; }

        public int SitePageID { get; set; }

        public bool PermissionApprove { get; set; }

        public virtual SitePage SitePage { get; set; }

        public virtual User User { get; set; }
    }
}
