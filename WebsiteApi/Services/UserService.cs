﻿using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;

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

        public bool check(string Email = "", string UserName = "", string Phone = "")
        {
            return _userRepository.check(Email, UserName, Phone);
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
            return _mapper.Map<UserDto>(_userRepository.Update(id, _mapper.Map<User>(user)));

        }
    }
}