using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.MongoEntities
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperationLog : MongoEntity
    {
        public string ClassName { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreateTime { get; set; }

        public string LogName { get; set; }

        public string LogType { get; set; }

        public string Message { get; set; }

        public string Method { get; set; }

        public string Succeed { get; set; }

        public long? UserId { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string RemoteIpAddress { get; set; }
    }
}
