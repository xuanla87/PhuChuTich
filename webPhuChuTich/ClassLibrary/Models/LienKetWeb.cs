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

    [Table("LienKetWeb")]
    public partial class LienKetWeb
    {
        [Key]
        public int lienKetWebId { get; set; }
        public string lienKetWebName { get; set; }
        public string lienKetWebLink { get; set; }
        public int isSort { get; set; }
    }
}
