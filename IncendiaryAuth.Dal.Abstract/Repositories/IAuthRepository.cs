using IncendiaryAuth.Dal.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Dal.Abstract.Repositories
{
    public interface IAuthRepository
    {
        UserDto GetUser(string userName);
    }
}
