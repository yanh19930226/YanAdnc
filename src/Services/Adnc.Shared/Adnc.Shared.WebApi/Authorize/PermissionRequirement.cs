using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Authorize
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Name { get; private set; }

        public PermissionRequirement()
        {
        }

        public PermissionRequirement(string name) => Name = name;
    }
}
