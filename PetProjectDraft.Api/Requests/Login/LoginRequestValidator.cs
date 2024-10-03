using FluentValidation;
using PetProjectDraft.Application.ComonValidators;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;

namespace PetProjectDraft.Api.Requests.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
             .NotEmptyWithError();

            RuleFor(x => x.Email).MustBeValueObject(Email.Create);

            RuleFor(x => x.Password)
             .NotEmptyWithError()
             .MinimumLengthWithError(Constraints.MINIMUM_SYMBOLS_FOR_PASSWORD);
        }
    }
}
