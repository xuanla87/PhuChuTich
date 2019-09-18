namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageModule
    {
        public int ImageModuleID { get; set; }

        public int ModuleID { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required]
        [StringLength(7)]
        public string TargetUrl { get; set; }

        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public virtual Module Module { get; set; }
    }
}
