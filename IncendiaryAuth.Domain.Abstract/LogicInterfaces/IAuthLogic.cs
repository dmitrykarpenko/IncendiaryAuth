using IncendiaryAuth.Domain.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Domain.Abstract.LogicInterfaces
{
    public interface IAuthLogic
    {
        UserModel AuthenticateUser(UserAuthModel userAuth);
    }
}
