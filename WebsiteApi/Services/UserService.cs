using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebsiteApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public string ChangePassword(int id, string password)
        {
            return _userRepository.ChangePassword(id, password);
        }

        public bool check(string Email = "", string UserName = "", string Phone = "")
        {
            return _userRepository.check(Email, UserName, Phone);
        }
        public string UploadImage(string path)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
            {
                string CLOUD_NAME = "tandn";
                string API_KEY = "661621979949236";
                string API_SECRET = "-QNMYjxVSCWpWhi0dLWj7G_hv_g";
                Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
                Cloudinary cloudinary = new Cloudinary(account);
                cloudinary.Api.Secure = true;
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(path)
                };
                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUri.ToString();
            }
            return string.Empty;
        }


        public string Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_userRepository.GetAll());
        }

        public UserDto GetById(int id)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetById(id));
            if (user is null)
            {
                throw new IsNotExist("There is no user with Id is " + id);
            }
            return user;
        }

        public UserDto Update(int id, UserDto user)
        {
            var u = this.GetById(id);
            if (u == null)
            {
                throw new IsNotExist("There is no user with Id is " + id);
            }
            if (!u.UserName.Equals(user.UserName) && !this.check(UserName: user.UserName))
            {
                throw new IsExist("Username is existed!");
            }
            if (!u.Email.Equals(user.Email) && !this.check(Email: user.Email))
            {
                throw new IsExist("Email is existed!");
            }
            if (!u.Phone.Equals(user.Phone) && !this.check(Phone: user.Phone))
            {
                throw new IsExist("Phone is existed!");
            }
            if (string.IsNullOrEmpty(user.ImagePath) || string.IsNullOrWhiteSpace(user.ImagePath))
            {
                user.ImagePath = u.ImagePath;
            }
            return _mapper.Map<UserDto>(_userRepository.Update(id, _mapper.Map<User>(user)));

        }
    }
}
