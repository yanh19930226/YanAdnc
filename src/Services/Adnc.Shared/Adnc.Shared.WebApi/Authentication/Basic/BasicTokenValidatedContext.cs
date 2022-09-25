using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Authentication.Basic
{
    public class BasicTokenValidatedContext : ResultContext<BasicSchemeOptions>
    {
        public BasicTokenValidatedContext(HttpContext context, AuthenticationScheme scheme, BasicSchemeOptions options)
            : base(context, scheme, options)
        {
        }
    }
}
