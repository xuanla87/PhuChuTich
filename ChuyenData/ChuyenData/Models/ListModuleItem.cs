namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advanced.ListModuleItems")]
    public partial class ListModuleItem
    {
        public int Id { get; set; }

        public int ModuleId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required]
        [StringLength(12)]
        public string Target { get; set; }

        [Required]
        [StringLength(255)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(255)]
        public string LargeThumbnail { get; set; }

        [Required]
        public string Description { get; set; }

        public int SortOrder { get; set; }

        public virtual Module Module { get; set; }
    }
}
