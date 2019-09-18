namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ModuleArticlePage
    {
        public int ModuleArticlePageID { get; set; }

        public int ArticleModuleID { get; set; }

        public int SitePageID { get; set; }

        public virtual ArticleModule ArticleModule { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
