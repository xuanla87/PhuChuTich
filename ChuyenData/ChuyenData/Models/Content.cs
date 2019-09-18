namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public int contentId { get; set; }

        public int? parentId { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        [StringLength(500)]
        public string alias { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(250)]
        public string thumbnail { get; set; }

        public bool allowComment { get; set; }

        public DateTime createTime { get; set; }

        public DateTime modifiedTime { get; set; }

        [Required]
        [StringLength(250)]
        public string createUser { get; set; }

        [Required]
        [StringLength(250)]
        public string modifiedUser { get; set; }

        [StringLength(250)]
        public string authorName { get; set; }

        public DateTime ngayDang { get; set; }

        public int isView { get; set; }

        public bool isTrash { get; set; }

        public bool approved { get; set; }

        public DateTime? approvedTime { get; set; }

        [StringLength(250)]
        public string approvedUser { get; set; }

        [Required]
        [StringLength(50)]
        public string contentKey { get; set; }

        public string contentMain { get; set; }

        public int languageId { get; set; }

        [StringLength(250)]
        public string metaDescription { get; set; }

        [StringLength(250)]
        public string metaKeyword { get; set; }

        [StringLength(250)]
        public string metaTitle { get; set; }

        public int isSort { get; set; }

        public bool isHome { get; set; }

        public bool isFeature { get; set; }

        public bool isNew { get; set; }

        public int? oldId { get; set; }
    }
}
