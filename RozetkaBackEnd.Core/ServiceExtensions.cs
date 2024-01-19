using Microsoft.Extensions.DependencyInjection;
using RozetkaBackEnd.Core.AutoMappers.Users;
using RozetkaBackEnd.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile)); 
        }
    }
}
