namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Content")]
    public partial class Content
    {
        [Key]
        public int contentId { get; set; }
        public int? parentId { get; set; }

        public string name { get; set; }
        public string alias { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public bool allowComment { get; set; }
        public DateTime createTime { get; set; }
        public DateTime modifiedTime { get; set; }
        public string createUser { get; set; }
        public string modifiedUser { get; set; }
        public string authorName { get; set; }
        public DateTime ngayDang { get; set; }
        public int isView { get; set; }
        public bool isTrash { get; set; }
        public bool approved { get; set; }
        public DateTime? approvedTime { get; set; }
        public string approvedUser { get; set; }
        public string contentKey { get; set; }
        public string contentMain { get; set; }
        public int languageId { get; set; }
        public string metaDescription { get; set; }
        public string metaKeyword { get; set; }
        public string metaTitle { get; set; }
        public int isSort { get; set; }
        public bool isHome { get; set; }
        public bool isFeature { get; set; }
        public bool isNew { get; set; }
        public int? oldId { get; set; }
    }
    public class ContentView
    {
        public int Total { set; get; }
        public int TotalRecord { set; get; }
        public IEnumerable<Content> ViewContents { set; get; }
    }
    public class DropdownModel
    {
        public string Text { set; get; }
        public long Value { set; get; }
        public bool IsSelect { get; set; }
    }
}
