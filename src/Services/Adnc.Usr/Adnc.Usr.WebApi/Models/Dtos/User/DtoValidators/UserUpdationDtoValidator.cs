using Adnc.Usr.WebApi.Models.Dtos.Users;
using FluentValidation;

namespace Adnc.Usr.Application.Contracts.DtoValidators
{
    public class UserUpdationDtoValidator : AbstractValidator<UserUpdationDto>
    {
        public UserUpdationDtoValidator()
        {
            Include(new UserCreationAndUpdationDtoValidator());
        }
    }
}