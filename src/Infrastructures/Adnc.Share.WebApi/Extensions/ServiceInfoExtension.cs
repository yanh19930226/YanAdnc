using Adnc.Infra.Core.Adnc.Interfaces;
using System.Reflection;

namespace Adnc.Share.WebApi.Extensions
{
    public static class ServiceInfoExtension
    {
        /// <summary>
        /// 获取WebApiAssembly程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly GetWebApiAssembly(this IServiceInfo serviceInfo) => serviceInfo.StartAssembly;

        /// <summary>
        /// 获取Application程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly GetApplicationAssembly(this IServiceInfo serviceInfo) => serviceInfo.StartAssembly;
    }
}
