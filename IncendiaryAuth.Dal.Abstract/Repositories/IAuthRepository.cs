using IncendiaryAuth.Dal.Abstract.Dtos;
using IncendiaryAuth.Dal.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Dal.Abstract.Repositories
{
    public interface IAuthRepository
    {
        //SignInInfoModel AddSuccessfulLogin(string userName);
        //SignInInfoModel AddFailedLogin(string userName);

        UserDto GetUser(string userName);
    }
}
