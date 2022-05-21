using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.TokenGenerator
{
    public interface ITokenGenerator
    {
        public string Scheme { get; }

        /// <summary>
        /// 创建一个token
        /// </summary>
        /// <returns></returns>
        public string Create();
    }
}
