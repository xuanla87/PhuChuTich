namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.ProductProperties")]
    public partial class ProductProperty
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PropertyDefinitionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual Product Product { get; set; }

        public virtual Property Property { get; set; }
    }
}
