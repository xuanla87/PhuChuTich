namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Newsletter
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime SubcribeDate { get; set; }
    }
}
