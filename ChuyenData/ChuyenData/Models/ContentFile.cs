namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContentFile
    {
        [Key]
        public int FileID { get; set; }

        public short ContentType { get; set; }

        public int ContentID { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }
    }
}
