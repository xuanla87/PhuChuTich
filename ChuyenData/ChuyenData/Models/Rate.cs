namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.Rates")]
    public partial class Rate
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Mark { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }
    }
}
