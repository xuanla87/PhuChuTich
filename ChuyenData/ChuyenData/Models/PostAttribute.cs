namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("News.PostAttributes")]
    public partial class PostAttribute
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(70)]
        public string AttributeName { get; set; }

        [Required]
        public string AttributeValue { get; set; }

        public virtual Post Post { get; set; }
    }
}
