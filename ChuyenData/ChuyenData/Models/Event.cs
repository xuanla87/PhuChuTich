namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        public int EventID { get; set; }

        public int SitePageID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        public bool Visible { get; set; }

        public bool Featured { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedByUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedByUser { get; set; }

        public DateTime ApprovedDate { get; set; }

        public virtual SitePage SitePage { get; set; }
    }
}
