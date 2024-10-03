using FluentValidation;
using PetProjectDraft.Application.ComonValidators;

namespace PetProjectDraft.Api.Requests.DeletePhoto
{
    public class DeleteVolunteerPhotoRequestValidator : AbstractValidator <DeleteVolunteerPhotoRequest>
    {
        public DeleteVolunteerPhotoRequestValidator()
        {
            RuleFor(x => x.VolunteerId)
              .NotEmptyWithError();

            RuleFor(x => x.Path)
              .NotEmptyWithError();
        }
    }
}
