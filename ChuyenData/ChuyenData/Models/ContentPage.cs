namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContentPage
    {
        public int ContentPageID { get; set; }

        public int SitePageID { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string PageContent { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
