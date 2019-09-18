namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Photos = new HashSet<Photo>();
        }

        public int AlbumID { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Visible { get; set; }

        public bool HomePage { get; set; }

        public int Views { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ApprovedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
