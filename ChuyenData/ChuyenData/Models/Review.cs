namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.Reviews")]
    public partial class Review
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime ReviewDate { get; set; }

        public DateTime ApprovedDate { get; set; }

        public bool Visible { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual Product Product { get; set; }
    }
}
