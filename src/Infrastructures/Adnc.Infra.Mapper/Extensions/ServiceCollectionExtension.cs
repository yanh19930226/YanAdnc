using Adnc.Infra.Mapper.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Mapper.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraAutoMapper(this IServiceCollection services, params Type[] profileAssemblyMarkerTypes)
        {
            services.AddAutoMapper(profileAssemblyMarkerTypes);
            services.AddSingleton<IObjectMapper, AutoMapperObject>();
            return services;
        }

        public static IServiceCollection AddAdncInfraAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddAutoMapper(assemblies);
            services.AddSingleton<IObjectMapper, AutoMapperObject>();
            return services;
        }
    }
}
