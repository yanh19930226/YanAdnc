using Adnc.Shared.Application.Contracts.Dtos;
using System;

namespace Adnc.Usr.WebApi.Models.Vos.Users
{
    [Serializable]
    public record UserValidatedInfoVo : IDto
    {
        public UserValidatedInfoVo(long id, string account, string name, string roleids, int status)
        {
            Id = id;
            Account = account;
            Name = name;
            RoleIds = roleids;
            Status = status;
            ValidationVersion = Guid.NewGuid().ToString("N");
        }

        public long Id { get; init; }

        public string Account { get; init; }

        public string Name { get; init; }

        public string RoleIds { get; init; }

        public int Status { get; init; }

        public string ValidationVersion { get; init; }
    }
}
