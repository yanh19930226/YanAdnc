using Adnc.Infra.Caching.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Configurations
{
    /// <summary>
    /// Defines an endpoint.
    /// </summary>
    public class ServerEndPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Adnc.Infra.Caching.Configurations.ServerEndPoint"/> class.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="port">Port.</param>
        public ServerEndPoint(string host, int port)
        {
            ArgumentCheck.NotNullOrWhiteSpace(host, nameof(host));

            Host = host;
            Port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Adnc.Infra.Caching.Core.Configurations.ServerEndPoint"/> class.
        /// </summary>
        public ServerEndPoint()
        {
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }
    }
}
