using FluentValidation;
using PariPlayLeague.Application.Features.Teams.Commands;

namespace PariPlayLeague.Application.Validators
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(t => t.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Team name is required.")
               .MaximumLength(40);

            RuleFor(t => t.President)
               .NotNull()
               .NotEmpty()
               .WithMessage("President name is required.")
               .MaximumLength(45);
        }
    }
}
