using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace www.Areas.Admin.Models
{
    public class ModelDanhMuc
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        public bool isTrash { get; set; }
        public int isSort { get; set; }
    }
}