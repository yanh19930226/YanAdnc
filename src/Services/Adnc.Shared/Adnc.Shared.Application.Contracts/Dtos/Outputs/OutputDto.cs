using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Outputs
{
    [Serializable]
    public abstract class OutputDto : IOutputDto
    {
        public virtual long Id { get; set; }
    }
}
