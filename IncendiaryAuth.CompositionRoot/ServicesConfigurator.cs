using IncendiaryAuth.Dal.Abstract.Repositories;
using IncendiaryAuth.Dal.Repositories;
using IncendiaryAuth.Domain.Abstract.LogicInterfaces;
using IncendiaryAuth.Domain.LogicImplementations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IncendiaryAuth.CompositionRoot
{
    public static class ServicesConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthLogic, AuthLogic>();
        }
    }
}
