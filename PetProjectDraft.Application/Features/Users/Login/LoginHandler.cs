using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Features.Users.Login
{
    public class LoginHandler
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken ct)
        {
            var user = await _usersRepository.GetByEmail(command.Email, ct);

            if (user.IsFailure)
                return user.Error;

            var isVerified = BCrypt.Net.BCrypt.Verify(command.Password, user.Value.PasswordHash);
            if (isVerified == false)
                return Errors.Users.InvalidCredentials();

            var token = _jwtProvider.Generate(user.Value);

            var response = new LoginResponse(token.Value, user.Value.Role.Name);
            return response;
        }
    }

}
