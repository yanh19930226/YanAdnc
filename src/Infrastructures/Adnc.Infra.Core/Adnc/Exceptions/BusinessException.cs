using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Exceptions
{
    public class BusinessException : Exception, IAdncException
    {
        public BusinessException(string message)
            : base(message)
        {
            base.HResult = (int)HttpStatusCode.Forbidden;
        }

        public BusinessException(HttpStatusCode statusCode, string message)
        : base(message)
        {
            base.HResult = (int)statusCode;
        }
    }
}
