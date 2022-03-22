﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("Image")]
    public class Image
    {
        public Image()
        {
            ProductImages = new HashSet<ProductImage>();
            CommentImages = new HashSet<CommentImage>();
        }
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        public string Label { get; set; }
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Del { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<CommentImage> CommentImages { get; set; }

    }
}
