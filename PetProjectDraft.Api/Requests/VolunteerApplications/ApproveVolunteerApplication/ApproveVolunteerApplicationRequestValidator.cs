using FluentValidation;
using PetProjectDraft.Application.ComonValidators;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Requests.VolunteerApplications.ApproveVolunteerApplication
{
    public class ApproveVolunteerApplicationRequestValidator : AbstractValidator<ApproveVolunteerApplicationRequest>
    {
        public ApproveVolunteerApplicationRequestValidator()
        {
            RuleFor(x => x.Id)
              .NotEmptyWithError();
        }
    }
}
