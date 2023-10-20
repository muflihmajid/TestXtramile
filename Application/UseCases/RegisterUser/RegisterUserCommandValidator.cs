using FluentValidation;

namespace SceletonAPI.Application.UseCases.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Data.email).NotEmpty().EmailAddress().WithMessage("5.5.4 Invalid Address");
            RuleFor(x => x.Data.phone).NotEmpty().WithMessage("Nomor Handphone harus diisi");
            // RuleFor(x => x.Data.company).NotEmpty().WithMessage("Company harus diisi");
            // RuleFor(x => x.Data.name).NotEmpty().WithMessage("Nama harus diisi");
        }
    }
}
