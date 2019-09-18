namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.ProductFAQs")]
    public partial class ProductFAQ
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(500)]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        public int SortOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
