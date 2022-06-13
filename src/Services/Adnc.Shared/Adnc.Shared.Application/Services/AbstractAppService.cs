using Adnc.Infra.Mapper;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Shared.Application.Contracts.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Services
{
    public abstract class AbstractAppService : IAppService
    {
        public IObjectMapper Mapper
        {
            get
            {
                var httpContext = HttpContextHelper.GetCurrentHttpContext();
                if (httpContext is not null)
                    return httpContext.RequestServices.GetRequiredService<IObjectMapper>();
                if (ServiceLocator.Provider is not null)
                    return ServiceLocator.Provider.GetService<IObjectMapper>();
                throw new NotImplementedException();
            }
        }

        protected AppSrvResult AppSrvResult() => new();

        protected AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value) => new(value);

        protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null) => new(statusCode, detail, title, instance, type);

        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions) => expressions;
    }
}
