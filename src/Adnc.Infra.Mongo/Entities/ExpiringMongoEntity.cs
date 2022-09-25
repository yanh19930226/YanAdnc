using Adnc.Infra.Repository.Entities;
using Adnc.Infra.Repository.Entities.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Mongo.Entities
{
    /// <summary>
    /// A mongo entity that will be automatically deleted after a configured time has elapsed.
    /// </summary>
    /// <seealso cref="MongoEntity" />
    public abstract class ExpiringMongoEntity : MongoEntity
    {
        /// <summary>
        /// Gets or sets the date at which this entity was created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
