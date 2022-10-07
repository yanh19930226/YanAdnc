namespace Adnc.Usr.WebApi.Models.Dtos.Users
{
    public class UserCreationDto : UserCreationAndUpdationDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}