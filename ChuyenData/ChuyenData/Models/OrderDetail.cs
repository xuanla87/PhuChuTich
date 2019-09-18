namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.OrderDetails")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductUnit { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int PromotionPrice { get; set; }

        [Required]
        [StringLength(300)]
        public string Note { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
