﻿using Adnc.Infra.Core.Adnc.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EventBus.RabbitMq
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }

    internal sealed class RabbitMqConnection : IRabbitMqConnection
    {
        private static volatile RabbitMqConnection? _uniqueInstance;
        private static readonly object _lockObject = new();
        private ILogger<dynamic> _logger = default!;
        public IConnection Connection { get; private set; } = default!;

        private RabbitMqConnection()
        {

        }

        internal static RabbitMqConnection GetInstance(IOptions<RabbitMqConfig> options, string clientProvidedName, ILogger<dynamic> logger)
        {
            if (_uniqueInstance is null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance is null)
                    {
                        _uniqueInstance = new RabbitMqConnection(options, clientProvidedName, logger);
                    }
                }
            }
            return _uniqueInstance;
        }

        private RabbitMqConnection(IOptions<RabbitMqConfig> options, string clientProvidedName, ILogger<dynamic> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory()
            {
                ClientProvidedName = clientProvidedName,
                HostName = options.Value.HostName,
                VirtualHost = options.Value.VirtualHost,
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                Port = options.Value.Port,
                //Rabbitmq集群必需加这两个参数
                AutomaticRecoveryEnabled = true,
                //TopologyRecoveryEnabled=true
            };

            Policy.Handle<SocketException>()
                  .Or<BrokerUnreachableException>()
                  .WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(1), (ex, time, retryCount, content) =>
                  {
                      if (2 == retryCount)
                          throw ex;
                      _logger.LogError(ex, string.Format("{0}:{1}", retryCount, ex.Message));
                  })
                  .Execute(() =>
                  {
                      Connection = factory.CreateConnection();
                  });
        }
    }
}
