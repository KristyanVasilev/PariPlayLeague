using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PariPlayLeague.API.Constants;
using PariPlayLeague.API.Contracts.Requests.Seasons;
using PariPlayLeague.API.Contracts.Responses.Seasons;
using PariPlayLeague.Application.Features.Seasons.Commands;
using PariPlayLeague.Application.Features.Seasons.Queries;
using PariPlayLeague.Application.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;

namespace PariPlayLeague.API.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeasonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Season
        /// </summary>
        [HttpPost("/api/v1/seasons")]
        [SwaggerResponse(201, "season created")]
        [SwaggerResponse(400, "Unable to create season")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonRequest request, CancellationToken cancellationToken = default)
        {
            var command = CreateSeasonRequest.MapToCommand(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = CreateSeasonResponse.MapToResponse(result.Data);

            return Ok(response);
        }

        /// <summary>
        /// Create Season Matches
        /// </summary>
        [HttpPost("/api/v1/seasons/{id:guid}/matches")]
        [SwaggerResponse(201, "season created", typeof(ScheduleSeasonMatchesCommand))]
        [SwaggerResponse(400, "Unable to create season")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> CreateSeasonMatches([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var command = new ScheduleSeasonMatchesCommand(id);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieve Season past Matches
        /// </summary>
        [HttpGet("/api/v1/seasons/past-matches")]
        [SwaggerResponse(201, "season past matches retrieved", typeof(GetPastMatchesResponse))]
        [SwaggerResponse(400, "Unable to retrieve season past matches")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> GetPastMatches([FromQuery] GetMatchesFilter request, CancellationToken cancellationToken = default)
        {
            var command = GetSeasonPastMatchesRequest.MapToQuery(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = GetPastMatchesResponse.MapToResponse(result.Data);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve Season next Matches
        /// </summary>
        [HttpGet("/api/v1/seasons/next-matches")]
        [SwaggerResponse(201, "season past matches retrieved", typeof(GetPastMatchesResponse))]
        [SwaggerResponse(400, "Unable to retrieve season next matches")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> GetNextMatches([FromQuery] GetMatchesFilter request, CancellationToken cancellationToken = default)
        {
            var command = GetSeasonNextMatchesRequest.MapToQuery(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var response = GetNextMatchesResponse.MapToResponse(result.Data);

            return Ok(response);
        }

        /// <summary>
        /// Get Current Season Standings
        /// </summary>
        [HttpGet("/api/v1/seasons/standings")]
        [SwaggerResponse(201, "current season standings retrieved", typeof(List<DTOStandings>))]
        [SwaggerResponse(400, "Unable to retrieve current season standings")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> GetCurrentSeasonStandings(CancellationToken cancellationToken = default)
        {
            var query = new GetCurrentSeasonStandins();

            var result = await _mediator.Send(query, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// update match result
        /// </summary>
        [HttpPatch("/api/v1/seasons/matches")]
        [SwaggerResponse(201, "match result updatet")]
        [SwaggerResponse(400, "Unable to update match result")]
        [SwaggerResponse(422, "Validation exception")]
        [SwaggerOperation(Tags = [EndpointTags.SEASON_TAG])]
        public async Task<IActionResult> UpdateMatchResult([FromBody] MatchResultRequest request, CancellationToken cancellationToken = default)
        {
            var command = MatchResultRequest.MapToCommand(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }
    }
}
