using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly ApiContext _context;
        private readonly ITokenService _tokenService;
        public AccountsController(ApiContext context, ITokenService tokenService, IUserService userService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        #region POST
        /// <summary>
        /// Perform information check when the user logs into the system
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ActionResult<string> Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            user.Role = _context.Roles.Where(p => p.Id == user.RoleId).FirstOrDefault();

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                avt = user.ImagePath,
                Username = user.UserName,
                Role = user.Role.Name,
                Token = _tokenService.CreateToken(user)
            });
        }

        /// <summary>
        /// Provide accounts for users
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDto registerDto)
        {
            registerDto.UserName.ToLower();
            if (_context.Users.Any(u => u.UserName.Equals(registerDto.UserName)))
            {
                return BadRequest("Username is existed!");
            }
            if (_context.Users.Any(u => u.Email.Equals(registerDto.Email)))
            {
                return BadRequest("Email is existed!");
            }
            if (_context.Users.Any(u => u.Phone.Equals(registerDto.Phone)))
            {
                return BadRequest("Phone is existed!");
            }
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Address = registerDto.Address,
                FullName = registerDto.FullName,
                CreatedDate = DateTime.Now,
                ImagePath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdsRv7DIKFauJT3Djb82qGRCv2lpbieAdI9o84elfQ17_k69N_4p2Xd9XCgQzD0Jo351Y&usqp=CAU",
                Del = false
            };
            user.RoleId = _context.Roles.Where(x => string.Equals(x.Name, "USER")).FirstOrDefault().Id;
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                avt = user.ImagePath,
                Username = user.UserName,
                Role = user.Role.Name,
                Token = _tokenService.CreateToken(user)
            });
        }
        #endregion
    }
}
