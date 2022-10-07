using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.WebApi.Models.Dtos.Users
{
    public class UserChangeStatusDto : IDto
    {
        public long[] UserIds { get; set; }

        public int Status { get; set; }
    }
}