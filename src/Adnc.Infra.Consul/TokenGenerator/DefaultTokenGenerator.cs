using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.TokenGenerator
{
    public class DefaultTokenGenerator : ITokenGenerator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultTokenGenerator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Scheme => "Bearer";

        public string Create()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var tokenTxt = token?.Remove(0, 7);
            return tokenTxt;
        }
    }
}
