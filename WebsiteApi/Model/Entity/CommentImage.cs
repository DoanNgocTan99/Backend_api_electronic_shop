using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebsiteApi.Model.Entity
{
    [Table("CommentImage")]
    public class CommentImage
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long ImageId { get; set; }
        public virtual Image Image { get; set; }

        public long CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IdDel { get; set; }
    }
}
