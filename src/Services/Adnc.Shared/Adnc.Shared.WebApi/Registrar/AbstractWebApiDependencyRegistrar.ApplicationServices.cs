using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.System.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册Application层服务
        /// </summary>
        protected virtual void AddApplicationServices()  
        {
            var appAssembly = ServiceInfo.GetApplicationAssembly();
            if (appAssembly is not null)
            {
                var applicationRegistrarType = appAssembly.ExportedTypes.FirstOrDefault(m => m.IsAssignableTo(typeof(IDependencyRegistrar)) && m.IsNotAbstractClass(true));
                if (applicationRegistrarType is not null)
                {
                    var applicationRegistrar = Activator.CreateInstance(applicationRegistrarType, Services) as IDependencyRegistrar;
                    applicationRegistrar?.AddAdnc();
                }
            }
        }
    }
}
