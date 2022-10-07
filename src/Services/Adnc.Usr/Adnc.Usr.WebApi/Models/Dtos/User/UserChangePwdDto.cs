using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.WebApi.Models.Dtos.Users
{
    /// <summary>
    /// 修改密码数据模型
    /// </summary>
    public class UserChangePwdDto : IDto
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string RePassword { get; set; }
    }
}