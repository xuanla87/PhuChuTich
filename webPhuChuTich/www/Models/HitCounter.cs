namespace www.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HitCounter")]
    public partial class HitCounter
    {
        public int id { get; set; }

        public DateTime visitTime { get; set; }

        [StringLength(50)]
        public string visitIp { get; set; }

        [StringLength(255)]
        public string visitBrowser { get; set; }

        [StringLength(255)]
        public string visitPage { get; set; }
    }
}
