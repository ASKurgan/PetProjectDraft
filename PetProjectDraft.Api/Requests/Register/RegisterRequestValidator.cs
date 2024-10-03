using FluentValidation;
using PetProjectDraft.Application.ComonValidators;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;

namespace PetProjectDraft.Api.Requests.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterRequestValidator()
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
