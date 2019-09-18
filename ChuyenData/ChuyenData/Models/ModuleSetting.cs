namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ModuleSetting
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(70)]
        public string SettingName { get; set; }

        [Required]
        public string SettingValue { get; set; }

        public virtual Module Module { get; set; }
    }
}
