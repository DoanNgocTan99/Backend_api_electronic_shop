using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApi.Model.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime? DOB { get; set; }

        public string Address { get; set; }

        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long RoleId { get; set; }

        public bool Del { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
