using PetProjectDraft.Api.Requests.DeletePhoto;
using PetProjectDraft.Api.Requests.VolunteerApplications.ApproveVolunteerApplication;
using PetProjectDraft.Application.Features.VolunteerApplications.ApproveVolunteerApplication;
using PetProjectDraft.Application.Features.Volunteers.DeletePhoto;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Mapping.FromRequestToCommand
{
    public class ApproveVolunteerApplicationMapper
    {
        public static Result<ApproveVolunteerApplicationCommand> GetApproveVolunteerApplicationCommand
                                                                           (ApproveVolunteerApplicationRequest request,
                                                                            CancellationToken ct)
        {
            return new ApproveVolunteerApplicationCommand(request.Id);
        }
    }
}
