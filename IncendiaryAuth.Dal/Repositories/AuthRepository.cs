﻿using IncendiaryAuth.Dal.Abstract.Dtos;
using IncendiaryAuth.Dal.Abstract.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncendiaryAuth.Dal.Abstract.Models;

namespace IncendiaryAuth.Dal.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private IEnumerable<UserDto> _users = null;
        private IEnumerable<UserDto> Users => _users ?? (_users = GetAllUsers());

        // Considering failed sign in counts as if the counts are e.g. saved in a database

        private ConcurrentDictionary<string, SignInInfoModel> _signInsByUserNames =
            new ConcurrentDictionary<string, SignInInfoModel>();

        public SignInInfoModel AddSuccessfulLogin(string userName)
        {
            var signInInfo = GetSignInInfo(userName);
            if (signInInfo.FailedCount > 0)
                signInInfo.FailedCount = 0;
            return signInInfo;
        }

        public SignInInfoModel AddFailedLogin(string userName)
        {
            var signInInfo = GetSignInInfo(userName);
            ++signInInfo.FailedCount;
            return signInInfo;
        }

        public SignInInfoModel GetSignInInfo(string userName) =>
             _signInsByUserNames
                .GetOrAdd(userName.ToUpper(), new SignInInfoModel { FailedCount = 0 });

        public UserDto GetUser(string userName) =>
            !string.IsNullOrWhiteSpace(userName)
                ? Users.FirstOrDefault(u => u.UserName.ToUpper() == userName.ToUpper())
                : null;

        private IEnumerable<UserDto> GetAllUsers()
        {
            var streamReader = new StreamReader("../IncendiaryAuth.Dal/Files/users.json");
            var data = streamReader.ReadToEnd();
            var users = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(data);
            return users;
        }
    }
}
