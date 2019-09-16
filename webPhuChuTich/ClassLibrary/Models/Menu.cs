namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menus")]
    public partial class Menu
    {
        [Key]
        public int menuId { get; set; }
        public int? menuParentId { get; set; }
        public string menuName { get; set; }
        public string menuLink { get; set; }
        public bool isTrash { get; set; }
        public int isSort { get; set; }
        public int languageId { get; set; }
    }
    public class MenuView
    {
        public int Total { set; get; }
        public IEnumerable<Menu> ViewMenus { set; get; }
    }
}
