using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.WebApi.Models.Dtos.Users
{
    public class UserSetRoleDto : IDto
    {
        public long[] RoleIds { get; set; }
    }
}