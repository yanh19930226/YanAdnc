﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 需要把事务共享给CAP
        /// </summary>
        public bool SharedToCap { get; set; }
    }
}
