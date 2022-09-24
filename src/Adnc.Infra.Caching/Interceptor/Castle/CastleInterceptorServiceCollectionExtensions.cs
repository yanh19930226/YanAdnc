using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Interceptor.Castle
{
    public static class CastleInterceptorServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the castle interceptor.
        /// </summary>
        /// <returns>The castle interceptor.</returns>
        /// <param name="services">Services.</param>
        /// <param name="options">Adnc.Infra.Caching Interceptor config</param>
        //public static void ConfigureCastleInterceptor(this IServiceCollection services, Action<Adnc.Infra.CachingInterceptorOptions> options)
        //{
        //    services.TryAddSingleton<IAdnc.Infra.CachingKeyGenerator, DefaultAdnc.Infra.CachingKeyGenerator>();
        //    services.Configure(options);
        //}
    }
}
