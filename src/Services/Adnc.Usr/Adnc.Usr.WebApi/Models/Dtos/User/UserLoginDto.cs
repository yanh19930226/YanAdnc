using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.WebApi.Models.Dtos.Users
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class UserLoginDto : IDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}