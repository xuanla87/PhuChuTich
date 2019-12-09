namespace www.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        public int menuId { get; set; }

        [StringLength(250)]
        public string menuName { get; set; }

        [StringLength(250)]
        public string menuLink { get; set; }

        public int? menuParentId { get; set; }

        public int isSort { get; set; }

        public bool? isTrash { get; set; }

        public int languageId { get; set; }
    }
}
