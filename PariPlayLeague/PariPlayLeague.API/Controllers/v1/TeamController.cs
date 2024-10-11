using MediatR;
using Microsoft.AspNetCore.Mvc;
using PariPlayLeague.API.Constants;
using PariPlayLeague.API.Contracts.Requests.Teams;
using PariPlayLeague.API.Contracts.Responses.Teams;
using PariPlayLeague.Application.Features.Teams.Commands;
using PariPlayLeague.Application.Features.Teams.Queries;
using PariPlayLeague.Application.Filters;
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

        /// <summary>
        /// delete team
        /// </summary>
        [HttpDelete("/api/v1/teams/{id}")]
        [SwaggerResponse(200, "Request deleted")]
        [SwaggerResponse(400, "Unable to delete team")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.TEAM_TAG])]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid teamId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteTeamCommand(teamId), cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Retrieve team by id
        /// </summary>
        [HttpGet("/api/v1/teams/{id}")]
        [SwaggerResponse(201, "team retrieved", typeof(GetTeamByIdResponse))]
        [SwaggerResponse(400, "Unable to retrieve team")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.TEAM_TAG])]
        public async Task<IActionResult> GetTeamById([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetTeamByIdQuery(id);

            var result = await _mediator.Send(query, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = GetTeamByIdResponse.MapToResponse(result.Data);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve all teams
        /// </summary>
        [HttpGet("/api/v1/teams")]
        [SwaggerResponse(201, "teams retrieves", typeof(GetAllTeamResponse))]
        [SwaggerResponse(400, "Unable to retrieve teams")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.TEAM_TAG])]
        public async Task<IActionResult> GetAllTeams([FromQuery] GetAllTeamsFilter request, CancellationToken cancellationToken = default)
        {
            var command = GetAllTeamRequest.MapToQuery(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = GetAllTeamResponse.MapToResponse(result.Data);

            return Ok(response);
        }
    }
}
