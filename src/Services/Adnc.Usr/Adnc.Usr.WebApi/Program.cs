using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adnc.Usr.WebApi
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug($"init {nameof(Program.Main)}");
            try
            {
                var webApiAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                var serviceInfo = Adnc.Shared.WebApi.Registrar.ServiceInfo.CreateInstance(webApiAssembly);

                //Configuration,ServiceCollection,Logging,WebHost(Kestrel)
                var app = WebApplication
                    .CreateBuilder(args)
                    .ConfigureAdncDefault(serviceInfo)
                    .Build();

                //Middlewares
                app.UseAdnc();

                //Start
                await app
                    .ChangeThreadPoolSettings()
                    //.UseRegistrationCenter()
                    .RunAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }
    }
}
