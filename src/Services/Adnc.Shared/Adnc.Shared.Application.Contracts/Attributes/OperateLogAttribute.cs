using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class OperateLogAttribute : Attribute
    {
        public string LogName { get; set; }
    }
}
