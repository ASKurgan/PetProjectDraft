using PetProjectDraft.Api.Requests.DeletePhoto;
using PetProjectDraft.Api.Requests.Register;
using PetProjectDraft.Application.Features.Users.Register;
using PetProjectDraft.Application.Features.Volunteers.DeletePhoto;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Mapping.FromRequestToCommand
{
    public class RegisterMapper
    {
        public static Result<RegisterCommand> GetRegisterCommand(RegisterUserRequest request,
                                                                            CancellationToken ct)
        {
            return new RegisterCommand(request.Email, request.Password);
        }
    }
}
