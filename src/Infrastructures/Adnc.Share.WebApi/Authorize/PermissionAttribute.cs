using Adnc.Infra.Core.System.Extensions.String;
using Adnc.Shared.WebApi.Authentication.Basic;
using Adnc.Shared.WebApi.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Adnc.Shared.WebApi.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
[Obsolete("please use AdncAuthorizeAttribute")]
public class PermissionAttribute : AuthorizeAttribute
{
    public const string JwtWithBasicSchemes = $"{JwtBearerDefaults.AuthenticationScheme},{BasicDefaults.AuthenticationScheme}";

    public string[] Codes { get; set; }

    public PermissionAttribute(string code, string schemes = JwtBearerDefaults.AuthenticationScheme)
        : this(new string[] { code }, schemes)
    {
    }

    public PermissionAttribute(string[] codes, string schemes = JwtBearerDefaults.AuthenticationScheme)
    {
        Codes = codes;
        Policy = AuthorizePolicy.Default;
        if (schemes.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(schemes));
        else
            AuthenticationSchemes = schemes;
    }
}