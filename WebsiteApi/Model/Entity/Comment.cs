using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebsiteApi.Model.Entity
{
    [Table("Comment")]
    public class Comment
    {
        public Comment()
        {
            Images = new HashSet<Image>();
        }
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public long UserId { get; set; }

        public long ProductId { get; set; }

        public bool Del { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
    }
}
