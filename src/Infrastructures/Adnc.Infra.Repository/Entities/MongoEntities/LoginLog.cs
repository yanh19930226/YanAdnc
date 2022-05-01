using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.MongoEntities
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLog : MongoEntity
    {
        public string Device { get; set; }

        public string Message { get; set; }

        public bool Succeed { get; set; }

        public int StatusCode { get; set; }

        public long? UserId { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string RemoteIpAddress { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreateTime { get; set; }
    }
}
