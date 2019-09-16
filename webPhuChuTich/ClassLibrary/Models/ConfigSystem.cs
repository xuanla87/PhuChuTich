namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigSystem")]
    public partial class ConfigSystem
    {
        [Key]
        public int configId { get; set; }
        public string configKey { get; set; }
        public string configValue { get; set; }
        public bool isTrash { get; set; }
    }
}
