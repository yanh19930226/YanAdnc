﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Interceptor
{
    /// <summary>
    /// Adnc.Infra.Caching able attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAbleAttribute : CachingInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets the expiration. The default value is 30 second.
        /// </summary>
        /// <value>The expiration.</value>
        public int Expiration { get; set; } = 30;
    }
}
