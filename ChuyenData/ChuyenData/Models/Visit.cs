namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Visits { get; set; }
    }
}
