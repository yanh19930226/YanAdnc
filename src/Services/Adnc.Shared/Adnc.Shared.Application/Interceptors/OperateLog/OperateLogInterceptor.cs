using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Interceptors.OperateLog
{
    public class OperateLogInterceptor : IInterceptor
    {
        private readonly OperateLogAsyncInterceptor _opsLogAsyncInterceptor;

        public OperateLogInterceptor(OperateLogAsyncInterceptor opsLogAsyncInterceptor)
        {
            _opsLogAsyncInterceptor = opsLogAsyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _opsLogAsyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}
