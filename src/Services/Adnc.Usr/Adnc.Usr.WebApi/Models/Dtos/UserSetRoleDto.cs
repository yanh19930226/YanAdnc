using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.Application.Contracts.Dtos
{
    public class UserSetRoleDto : IDto
    {
        public long[] RoleIds { get; set; }
    }
}