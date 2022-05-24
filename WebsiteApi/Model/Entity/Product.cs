using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{

    [Table("Product")]
    public class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
            Transactionns = new HashSet<Transaction>();
            Carts = new HashSet<Cart>();
            Ratings = new HashSet<Rating>();
            ProductImages = new HashSet<ProductImage>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "Text")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Material { get; set; }

        [StringLength(250)]
        public string Origin { get; set; }

        public decimal Product_Price { get; set; }

        public decimal Del_Price { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public int? Stock { get; set; }

        public int? Discount { get; set; }

        public int? Views { get; set; }

        public int? Rate { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Del { get; set; }

        public long? BrandId { get; set; }

        public long CategoryId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }
        public string Avt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactionns { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
