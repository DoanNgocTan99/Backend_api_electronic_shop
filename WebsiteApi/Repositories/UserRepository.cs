using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiContext _context;
        public UserRepository(ApiContext context)
        {
            this._context = context;
        }

        public string ChangePassword(int id, string password)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new Exception("Invalid Password");
            }
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;
            _context.SaveChanges();
            return "Change password successfully";
        }

        /// <summary>
        /// Method validate Email, UserName, PhoneNumber. 
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="UserName"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public bool check(string Email = "", string UserName = "", string Phone = "")
        {
            if (!Email.Equals(""))
            {
                if (_context.Users.Where(x => x.Email.Equals(Email)).FirstOrDefault() != null)
                {
                    return false;
                }
            }

            if (!UserName.Equals(""))
            {
                if (_context.Users.Where(x => x.UserName.Equals(UserName)).FirstOrDefault() != null)
                {
                    return false;
                }
            }

            if (!Phone.Equals(""))
            {
                if (_context.Users.Where(x => x.Phone.Equals(Phone)).FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Delete User by IdUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="IsNotExist"></exception>
        public string Delete(int id)
        {
            var userDto = this.GetById(id);
            if (userDto == null)
                throw new IsNotExist("There is no user with Id is " + id);
            _context.Users.Remove(userDto);
            _context.SaveChanges();
            return "Delete successfully";
        }

        /// <summary>
        /// Get all User from table User
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        /// <summary>
        /// Get All User By IdUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Update profile User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Update(int id, User user)
        {
            user.UserName.ToLower();
            if (!_context.Users.Any(u => u.UserName.Equals(user.UserName)))
            {
                if (!check(UserName: user.UserName))
                {
                    throw new IsExist("Username is existed!");
                }
            }
            if (!_context.Users.Any(u => u.Email.Equals(user.Email)))
            {
                if (!check(Email: user.Email))
                {
                    throw new IsExist("Email is existed!");
                }
            }
            if (!_context.Users.Any(u => u.Phone.Equals(user.Phone)))
            {
                if (!check(Phone: user.Phone))
                {
                    throw new IsExist("Phone is existed!");
                }
            }
            var u = this.GetById(id);
            u.FullName = user.FullName;
            u.UserName = user.UserName;
            u.Email = user.Email;
            u.Phone = user.Phone;
            u.Gender = user.Gender;
            u.DOB = user.DOB;
            u.Address = user.Address;
            u.ImagePath = user.ImagePath;
            u.ModifiedDate = DateTime.Now;
            _context.SaveChanges();
            return u;
        }
    }
}
