using PetProjectDraft.Api.Requests.Login;
using PetProjectDraft.Api.Requests.Register;
using PetProjectDraft.Application.Features.Users.Login;
using PetProjectDraft.Application.Features.Users.Register;
using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Mapping.FromRequestToCommand
{
    public class LoginMapper
    {
        public static Result<LoginCommand> GetLoginCommand(LoginUserRequest request,
                                                                          CancellationToken ct)
        {
            return new LoginCommand(request.Email, request.Password);
        }
    }
}
