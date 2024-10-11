using FluentValidation;
using MediatR;

namespace PariPlayLeague.Application.Validators
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = _validator
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var errorFieldsMessages = failures.Select(x => x.ErrorMessage + ", ").ToList();
                throw new ValidationException(String.Join(string.Empty, errorFieldsMessages));
            }

            return await next();
        }
    }
}
