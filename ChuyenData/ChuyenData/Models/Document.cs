namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public int DocumentID { get; set; }

        public int DocumentCategoryID { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Signer { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime EffectDate { get; set; }

        [Required]
        [StringLength(255)]
        public string IssuingAgency { get; set; }

        [Required]
        [StringLength(1000)]
        public string Subject { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(300)]
        public string Attachment { get; set; }

        public bool Visible { get; set; }

        public bool HomePage { get; set; }

        public bool Featured { get; set; }

        public int Views { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateUser { get; set; }

        [Required]
        [StringLength(20)]
        public string ApprovedUser { get; set; }

        public DateTime ApprovedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedUser { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
