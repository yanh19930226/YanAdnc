﻿using Adnc.Infra.Repository.Entities.MongoEntities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Mongo.Entities
{
    /// <summary>
    /// A mongo entity with soft delete support.
    /// </summary>
    public abstract class SoftDeletableMongoEntity : MongoEntity
    {
        /// <summary>
        /// Gets or sets the date that this entity was soft deleted.
        /// Or null if it was not soft deleted.
        /// </summary>
        [BsonIgnoreIfNull]
        public DateTime? DateDeleted { get; set; }
    }
}
