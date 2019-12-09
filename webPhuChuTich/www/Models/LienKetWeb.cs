namespace www.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienKetWeb")]
    public partial class LienKetWeb
    {
        public int lienKetWebId { get; set; }

        [Required]
        [StringLength(250)]
        public string lienKetWebName { get; set; }

        [Required]
        [StringLength(500)]
        public string lienKetWebLink { get; set; }

        public int isSort { get; set; }
    }
}
