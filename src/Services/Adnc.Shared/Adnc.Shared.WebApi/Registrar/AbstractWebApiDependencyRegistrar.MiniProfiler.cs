using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册 MiniProfiler 组件
        /// </summary>
        protected virtual void AddMiniProfiler() =>
            Services
            .AddMiniProfiler(options => options.RouteBasePath = $"/{ServiceInfo.ShortName}/profiler")
            .AddEntityFramework();
    }
}
