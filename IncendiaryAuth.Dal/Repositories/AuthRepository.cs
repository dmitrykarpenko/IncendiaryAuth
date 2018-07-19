using IncendiaryAuth.Dal.Abstract.Dtos;
using IncendiaryAuth.Dal.Abstract.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Dal.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private IEnumerable<UserDto> _users = null;
        private IEnumerable<UserDto> Users => _users ?? (_users = GetAllUsers());

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
