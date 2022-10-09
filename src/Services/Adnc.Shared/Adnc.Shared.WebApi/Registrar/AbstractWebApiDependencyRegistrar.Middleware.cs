﻿using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.DependencyInjection;
using Adnc.Shared.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace Adnc.Shared.WebApi.Registrar
{
    //public abstract partial class ServiceRegistrar : IMiddlewareRegistrar
    //{
    //    protected readonly IApplicationBuilder App;
    //    protected ServiceRegistrar(IApplicationBuilder app)
    //    {
    //        App = app;
    //    }

    //    /// <summary>
    //    /// 注册中间件入口方法
    //    /// </summary>
    //    /// <param name="app"></param>
    //    public abstract void UseAdnc();

    //    /// <summary>
    //    /// 注册webapi通用中间件
    //    /// </summary>
    //    protected virtual void UseWebApiDefault(
    //        Action<IApplicationBuilder> beforeAuthentication = null,
    //        Action<IApplicationBuilder> afterAuthentication = null,
    //        Action<IApplicationBuilder> afterAuthorization = null,
    //        Action<IEndpointRouteBuilder> endpointRoute = null)
    //    {
    //        ServiceLocator.Provider = App.ApplicationServices;
    //        var environment = App.ApplicationServices.GetService<IHostEnvironment>();
    //        var serviceInfo = App.ApplicationServices.GetService<IServiceInfo>();
    //        var consulOptions = App.ApplicationServices.GetService<IOptions<ConsulConfig>>();

    //        var defaultFilesOptions = new DefaultFilesOptions();
    //        defaultFilesOptions.DefaultFileNames.Clear();
    //        defaultFilesOptions.DefaultFileNames.Add("index.html");
    //        App
    //            .UseDefaultFiles(defaultFilesOptions)
    //            .UseStaticFiles()
    //            .UseCustomExceptionHandler()
    //            .UseRealIp(x => x.HeaderKey = "X-Forwarded-For")
    //            .UseCors(serviceInfo.CorsPolicy);

    //        if (environment.IsDevelopment())
    //        {
    //            IdentityModelEventSource.ShowPII = true;
    //            App.UseMiniProfiler();
    //        }

    //        App
    //            .UseSwagger(c =>
    //            {
    //                c.RouteTemplate = $"/{serviceInfo.ShortName}/swagger/{{documentName}}/swagger.json";
    //                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    //                {
    //                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"/", Description = serviceInfo.Description } };
    //                });
    //            })
    //            .UseSwaggerUI(c =>
    //            {
    //                var assembly = serviceInfo.GetWebApiAssembly();
    //                c.IndexStream = () => assembly.GetManifestResourceStream($"{assembly.GetName().Name}.swagger_miniprofiler.html");
    //                c.SwaggerEndpoint($"/{serviceInfo.ShortName}/swagger/{serviceInfo.Version}/swagger.json", $"{serviceInfo.ServiceName}-{serviceInfo.Version}");
    //                c.RoutePrefix = $"{serviceInfo.ShortName}";
    //            })
    //            .UseHealthChecks($"/{consulOptions.Value.HealthCheckUrl}", new HealthCheckOptions()
    //            {
    //                Predicate = _ => true,
    //                // 该响应输出是一个json，包含所有检查项的详细检查结果
    //                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    //            })
    //            .UseRouting()
    //            .UseHttpMetrics();
    //        DotNetRuntimeStatsBuilder
    //        .Customize()
    //        .WithContentionStats()
    //        .WithGcStats()
    //        .WithThreadPoolStats()
    //        .StartCollecting();

    //        beforeAuthentication?.Invoke(App);
    //        App.UseAuthentication();
    //        afterAuthentication?.Invoke(App);
    //        App.UseAuthorization();
    //        afterAuthorization?.Invoke(App);

    //        App
    //            .UseEndpoints(endpoints =>
    //            {
    //                endpointRoute?.Invoke(endpoints);
    //                endpoints.MapMetrics();
    //                endpoints.MapControllers().RequireAuthorization();
    //            });
    //    }
    //}
}
