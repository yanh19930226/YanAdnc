using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Exceptions
{
    public class AdncArgumentNullException : ArgumentNullException, IAdncException
    {
        public AdncArgumentNullException()
            : base()
        {
        }

        public AdncArgumentNullException(string paramName)
            : base(paramName)
        {
        }

        public AdncArgumentNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AdncArgumentNullException(string paramName, string message)
            : base(paramName, message)
        {
        }
    }
}
