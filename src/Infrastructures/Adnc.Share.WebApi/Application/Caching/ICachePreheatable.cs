using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Share.WebApi.Application.Caching
{
    public interface ICachePreheatable
    {
        /// <summary>
        /// 预热缓存
        /// </summary>
        /// <returns></returns>
        Task PreheatAsync();
    }
}
