using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PetProjectDraft.Api.Mapping.FromRequestToCommand;
using PetProjectDraft.Api.Requests.Login;
using PetProjectDraft.Api.Requests.Register;
using PetProjectDraft.Application.Features.Users.Login;
using PetProjectDraft.Application.Features.Users.Register;

namespace PetProjectDraft.Api.Controllers
{
    public class UserController : ApplicationController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest request,
            [FromServices] LoginHandler handler,
            CancellationToken ct)
        {
            var commandResult = LoginMapper.GetLoginCommand(request, ct);

            if (commandResult.IsFailure)
            {
                return BadRequest(commandResult.Error);
            }

            var command = commandResult.Value;

            var token = await handler.Handle(command, ct);
            if (token.IsFailure)
                return BadRequest(token.Error);

            return Ok(token.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            [FromServices] RegisterHandler handler,
            CancellationToken ct)
        {
            var commandResult = RegisterMapper.GetRegisterCommand(request,ct);

            if (commandResult.IsFailure)
            {
                return BadRequest(commandResult.Error);
            }

            var command = commandResult.Value;

            var result = await handler.Handle(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
