using PetProjectDraft.Api.Requests.DeletePhoto;
using PetProjectDraft.Api.Requests.VolunteerApplications.ApplyVolunteerApplication;
using PetProjectDraft.Application.Features.VolunteerApplications.ApplyVolunteerApplication;
using PetProjectDraft.Application.Features.Volunteers.DeletePhoto;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Mapping.FromRequestToCommand
{
    public class ApplyVolunteerApplicationMapper
    {
        public static Result<ApplyVolunteerApplicationCommand> GetApplyVolunteerApplicationCommand(ApplyVolunteerApplicationRequest request,
                                                                            CancellationToken ct)
        {
            return new ApplyVolunteerApplicationCommand(request.FirstName, 
                                                        request.LastName,
                                                        request.Patronymic,
                                                        request.Email,
                                                        request.Description,
                                                        request.YearsExperience,
                                                        request.NumberOfPetsFoundHome,
                                                        request.FromShelter);
        }
    }
}
