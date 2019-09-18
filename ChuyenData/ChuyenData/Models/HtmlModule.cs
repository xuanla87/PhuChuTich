namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HtmlModule
    {
        public int HtmlModuleID { get; set; }

        public int ModuleID { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string HtmlContent { get; set; }

        public virtual Module Module { get; set; }
    }
}
