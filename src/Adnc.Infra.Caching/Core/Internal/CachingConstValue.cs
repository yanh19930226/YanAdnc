﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Internal
{
    /// <summary>
    /// Adnc.Infra.Caching const value.
    /// </summary>
    public class CachingConstValue
    {
        /// <summary>
        /// The config section.
        /// </summary>
        //public const string ConfigSection = "Adnc.Infra.Caching";
        /// <summary>
        /// The default name of the serializer.
        /// </summary>
        public const string DefaultSerializerName = "binary";

        public const string DefaultProtobufSerializerName = "proto";

        public const string DefaultJsonSerializerName = "json";

        public const string StackExchange = "redis.stackexchange";

        public const int PollyTimeout = 5;
    }
}
