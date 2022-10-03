using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.Application.Contracts.Dtos
{
    /// <summary>
    /// 刷新Token实体
    /// </summary>
    public class UserRefreshTokenDto : IDto
    {
        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}