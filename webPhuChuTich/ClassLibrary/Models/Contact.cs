namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Contacts")]
    public partial class Contact
    {
        [Key]
        public int contactId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        public string contactFullName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập thư điện tử")]
        public string contactEmail { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề")]
        public string contactTitle { get; set; }
        public string contactBody { get; set; }
        public bool isTrash { get; set; }
        public DateTime createTime { get; set; }
    }
    public class ContactView
    {
        public int Total { set; get; }
        public IEnumerable<Contact> ViewContacts { set; get; }
    }
}
