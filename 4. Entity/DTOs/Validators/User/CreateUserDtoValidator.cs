using DTOs.Buffalo.User;
using FluentValidation;

namespace DTOs.Validators.User
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull().EmailAddress();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.System).NotNull();
            RuleFor(x => x.DayOfBirth).NotNull();
            RuleFor(x => x.Sex).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull();
        }
    }
}
