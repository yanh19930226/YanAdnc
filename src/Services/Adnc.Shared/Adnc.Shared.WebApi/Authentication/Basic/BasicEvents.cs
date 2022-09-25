using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Authentication.Basic
{
    public class BasicEvents
    {
        public Func<BasicTokenValidatedContext, Task> OnTokenValidated { get; set; }
    }
}
