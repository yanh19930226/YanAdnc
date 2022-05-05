using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Configurations
{
    /// <summary>
    /// Redis database provider.
    /// </summary>
    public interface IRedisDatabaseProvider
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <returns>The database.</returns>
        IDatabase GetDatabase();

        /// <summary>
        /// Gets the server list.
        /// </summary>
        /// <returns>The server list.</returns>
        IEnumerable<IServer> GetServerList();

        string DBProviderName { get; }
    }
}
