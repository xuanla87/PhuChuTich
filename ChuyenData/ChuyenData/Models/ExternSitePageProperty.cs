namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExternSitePageProperty
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SitePageId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(70)]
        public string PropertyName { get; set; }

        [Required]
        public string PropertyValue { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
