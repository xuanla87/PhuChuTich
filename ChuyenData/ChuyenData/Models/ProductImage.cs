namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.ProductImages")]
    public partial class ProductImage
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string SmallImageUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string MediumImageUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string LargeImageUrl { get; set; }

        public int SortOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
