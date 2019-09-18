namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SiteSetting
    {
        [Key]
        [StringLength(70)]
        public string SettingName { get; set; }

        [Required]
        public string SettingValue { get; set; }
    }
}
