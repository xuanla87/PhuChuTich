namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Province
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }
}
