using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Authentication.Basic
{
    public class BasicPostConfigureOptions : IPostConfigureOptions<BasicSchemeOptions>
    {
        public void PostConfigure(string name, BasicSchemeOptions options)
        {
            // Method intentionally left empty.
        }
    }
}
