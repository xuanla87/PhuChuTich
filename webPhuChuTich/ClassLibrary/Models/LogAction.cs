namespace ClassLibrary.Models
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
        public string userAction { get; set; }
        public DateTime createTime { get; set; }
        public string ipAddress { get; set; }
        public string action { get; set; }
        public string browser { get; set; }
    }

    public class LogActionView
    {
        public int Total { set; get; }
        public int TotalRecord { set; get; }
        public IEnumerable<LogAction> ViewLogActions { set; get; }
    }
}
