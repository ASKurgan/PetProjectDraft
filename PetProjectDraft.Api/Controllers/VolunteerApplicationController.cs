using Microsoft.AspNetCore.Mvc;
using PetProjectDraft.Api.Mapping.FromRequestToCommand;
using PetProjectDraft.Api.Requests.VolunteerApplications.ApplyVolunteerApplication;
using PetProjectDraft.Api.Requests.VolunteerApplications.ApproveVolunteerApplication;
using PetProjectDraft.Application.Features.VolunteerApplications.ApplyVolunteerApplication;
using PetProjectDraft.Application.Features.VolunteerApplications.ApproveVolunteerApplication;

namespace PetProjectDraft.Api.Controllers
{
    public class VolunteerApplicationController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ApplyVolunteerApplicationHandler handler,
            [FromBody] ApplyVolunteerApplicationRequest request,
            CancellationToken ct)
        {
            var commandResult = ApplyVolunteerApplicationMapper.GetApplyVolunteerApplicationCommand
                                                                                        (request, ct);
            if (commandResult.IsFailure)
                return BadRequest(commandResult.Error);

            var command = commandResult.Value;
            var response = await handler.Handle(command, ct);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return Ok(response.Value);

            //  return Ok(); // заглушка
        }

        [HttpPost("approve")]
        public async Task<IActionResult> Approve(
            [FromServices] ApproveVolunteerApplicationHandler handler,
            [FromBody] ApproveVolunteerApplicationRequest request,
            CancellationToken ct)
        {
            var commandResult = ApproveVolunteerApplicationMapper.GetApproveVolunteerApplicationCommand
                                                                                            (request, ct);
            if (commandResult.IsFailure)
                return BadRequest(commandResult.Error);

            var command = commandResult.Value;

            var result = await handler.Handle(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
