using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos
{
    public abstract class MongoDto : IDto
    {
        public string Id { get; set; }
    }
}
