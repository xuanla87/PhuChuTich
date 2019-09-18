namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Photo
    {
        public int PhotoID { get; set; }

        public int AlbumID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(255)]
        public string PhotoUrl { get; set; }

        public int SortOrder { get; set; }

        public virtual Album Album { get; set; }
    }
}
