using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Option")]
    public partial class Option
    {
        [Key]
        public int optionId { get; set; }
        public int contentId { get; set; }
        public string thumbnail { get; set; }
        public string videoClip { get; set; }
        public int isSort { get; set; }
    }
}
