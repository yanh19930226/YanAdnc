using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Authentication.Basic
{
    public class BasicSchemeOptions : AuthenticationSchemeOptions
    {
        public BasicSchemeOptions()
        {
            Events = new BasicEvents();
        }

        public new BasicEvents Events
        {
            get { return (BasicEvents)base.Events; }
            set { base.Events = value; }
        }
    }
}
