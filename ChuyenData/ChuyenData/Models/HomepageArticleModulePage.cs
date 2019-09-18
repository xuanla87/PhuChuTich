namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HomepageArticleModulePage
    {
        public int HomepageArticleModulePageID { get; set; }

        public int? HomepageArticleModuleID { get; set; }

        public int? SitePageID { get; set; }

        public virtual HomepageArticleModule HomepageArticleModule { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
