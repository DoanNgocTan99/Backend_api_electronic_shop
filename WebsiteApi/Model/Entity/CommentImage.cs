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
        [StringLength(250)]
        public string Path { get; set; }

        public string Label { get; set; }
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
