using IncendiaryAuth.Dal.Abstract.Repositories;
using IncendiaryAuth.Domain.Abstract.LogicInterfaces;
using IncendiaryAuth.Domain.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Domain.LogicImplementations
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IAuthRepository _authRepository;

        private const byte _maxFailedCount = 3;

        public AuthLogic(
            IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public UserModel AuthenticateUser(UserAuthModel userAuth)
        {
            if (userAuth?.UserName == null || userAuth.Password == null)
            {
                return null;
            }

            var signInInfo = _authRepository.GetSignInInfo(userAuth.UserName);
            if (signInInfo.FailedCount >= _maxFailedCount)
            {
                return new UserModel { StatusHint = HttpStatusCode.NotFound };
            }

            var dto = _authRepository.GetUser(userAuth.UserName);
            // TODO: consider storing password hashes instead of passwords
            if (dto?.Password != userAuth.Password)
            {
                signInInfo = _authRepository.AddFailedLogin(userAuth.UserName);
                if (signInInfo.FailedCount >= _maxFailedCount)
                {
                    return new UserModel { StatusHint = HttpStatusCode.NotFound };
                }
                return null;
            }

            _authRepository.AddSuccessfulLogin(userAuth.UserName);

            // TODO: use AutoMapper
            return new UserModel
            {
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }
    }
}
