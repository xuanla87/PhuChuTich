namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Source
    {
        public int SourceID { get; set; }

        [Required]
        [StringLength(50)]
        public string SourceTitle { get; set; }
    }
}
