namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserArticlePages = new HashSet<UserArticlePage>();
            UserSiteZones = new HashSet<UserSiteZone>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Note { get; set; }

        public bool IsLock { get; set; }

        [Required]
        [StringLength(100)]
        public string Permission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserArticlePage> UserArticlePages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSiteZone> UserSiteZones { get; set; }
    }
}
