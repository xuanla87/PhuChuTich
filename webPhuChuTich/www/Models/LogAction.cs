namespace www.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogAction")]
    public partial class LogAction
    {
        [Key]
        public long logId { get; set; }

        [Required]
        [StringLength(50)]
        public string userAction { get; set; }

        public DateTime createTime { get; set; }

        [Required]
        [StringLength(50)]
        public string ipAddress { get; set; }

        [StringLength(500)]
        public string action { get; set; }

        [StringLength(250)]
        public string browser { get; set; }
    }
}
