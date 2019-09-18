namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store.Products")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductFAQs = new HashSet<ProductFAQ>();
            ProductImages = new HashSet<ProductImage>();
            ProductProperties = new HashSet<ProductProperty>();
            Rates = new HashSet<Rate>();
            Reviews = new HashSet<Review>();
            Categories = new HashSet<Category>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        public int BrandID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public int Price { get; set; }

        public int PromotionPrice { get; set; }

        public int Discount { get; set; }

        [Required]
        [StringLength(100)]
        public string Unit { get; set; }

        public int QuantityInStock { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Overview { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Specification { get; set; }

        public bool IsNew { get; set; }

        public bool IsPromotion { get; set; }

        public bool IsBestSelling { get; set; }

        public bool IsFeatured { get; set; }

        public bool HomePage { get; set; }

        public int Views { get; set; }

        public bool Visible { get; set; }

        public int ExGender { get; set; }

        public int ExAge { get; set; }

        public int ExMerterialID { get; set; }

        public DateTime ApprovedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedByUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedUser { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ExMerterial ExMerterial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFAQ> ProductFAQs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductProperty> ProductProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
