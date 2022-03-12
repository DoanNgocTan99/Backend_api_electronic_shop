using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? ProId { get; set; }

        public long? CateId { get; set; }

        public long? UserId { get; set; }

        public long? CommentId { get; set; }

        public bool Del { get; set; }

        public virtual Category Category { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
