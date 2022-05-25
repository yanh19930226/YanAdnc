﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EventBus.RabbitMq
{
    public enum ExchangeType
    {
        //发布订阅模式
        Fanout,

        //路由模式
        Direct,

        //通配符模式
        Topic
    }
}
