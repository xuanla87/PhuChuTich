namespace www.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigSytem")]
    public partial class ConfigSytem
    {
        [Key]
        public int configId { get; set; }

        [Required]
        [StringLength(50)]
        public string configKey { get; set; }

        public string configValue { get; set; }
    }
}
