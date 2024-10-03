using FluentValidation;
using PetProjectDraft.Api.Requests.PublishPet;
using PetProjectDraft.Application.ComonValidators;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.ValueObjects;

namespace PetProjectDraft.Api.Requests.VolunteerApplications.ApplyVolunteerApplication
{
    public class ApplyVolunteerApplicationRequestValidator : AbstractValidator<ApplyVolunteerApplicationRequest>
    {
        public ApplyVolunteerApplicationRequestValidator()
        {
            RuleFor(x => new { x.FirstName, x.LastName, x.Patronymic })
                   .MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Patronymic));

            RuleFor(x => x.Email).MustBeValueObject(Email.Create);

            RuleFor(x => x.Description)
               .NotEmptyWithError()
               .MaximumLengthWithError(Constraints.LONG_TITLE_LENGTH);

            RuleFor(x => x.YearsExperience)
               .NotEmptyWithError();

            RuleFor(x => x.FromShelter)
               .NotEmptyWithError();
        }
    }
}
