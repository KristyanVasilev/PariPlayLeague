using MediatR;
using Microsoft.AspNetCore.Mvc;
using PariPlayLeague.API.Constants;
using PariPlayLeague.API.Contracts.Requests.Teams;
using PariPlayLeague.API.Contracts.Responses.Teams;
using Swashbuckle.AspNetCore.Annotations;

namespace PariPlayLeague.API.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Create team
        /// </summary>
        [HttpPost("/api/v1/teams")]
        [SwaggerResponse(201, "team created", typeof(CreateTeamResponse))]
        [SwaggerResponse(400, "Unable to create team")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.TEAM_TAG])]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request, CancellationToken cancellationToken = default)
        {
            var command = CreateTeamRequest.MapToCommand(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = CreateTeamResponse.MapToResponse(result.Data);

            return Ok(response);
        }
    }
}
