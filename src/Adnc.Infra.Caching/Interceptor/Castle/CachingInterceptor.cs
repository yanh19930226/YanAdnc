using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Interceptor.Castle
{
    /// <summary>
    /// caching interceptor
    /// </summary>
    public class CachingInterceptor : IInterceptor
    {
        private readonly CachingAsyncInterceptor _cachingAsyncInterceptor;

        public CachingInterceptor(CachingAsyncInterceptor cachingAsyncInterceptor)
        {
            _cachingAsyncInterceptor = cachingAsyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _cachingAsyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}
