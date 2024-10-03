using PetProjectDraft.Api.Requests.DeletePhoto;
using PetProjectDraft.Application.Features.Volunteers.DeletePhoto;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Mapping.FromRequestToCommand
{
    public class DeleteVolunteerPhotoMapper
    {
        public static Result<DeleteVolunteerPhotoCommand> GetDeleteVolunteerPhotoCommand(DeleteVolunteerPhotoRequest request,
                                                                             CancellationToken ct)
        {
            return new DeleteVolunteerPhotoCommand(request.VolunteerId, request.Path);
        }
    }
}
