using MediatR;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Teams.Commands
{
    public class CreateTeamCommand : IRequest<Result<Team>>
    {
        public string Name { get; set; } = default!;
        public string President { get; set; } = default!;

        public static Team MapToEntity(CreateTeamCommand command)
        {
            return new Team
            {
                Name = command.Name,
                President = command.President,
                Champion = 0
            };
        }
    }

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<Team>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public CreateTeamCommandHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Team>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = CreateTeamCommand.MapToEntity(request);

            await _context.AddAsync(team, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult<Team>(team);
        }
    }
}
