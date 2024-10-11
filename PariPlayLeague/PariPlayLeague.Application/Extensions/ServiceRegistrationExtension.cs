using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PariPlayLeague.Application.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection ApplicationLayerRegistration(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //Validators
            //services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerCommandValidator>();
            //services.AddScoped<IValidator<CreateTeamCommand>, CreateTeamCommandValidator>();
           // services.AddScoped<IValidator<CreateSeasonCommand>, CreateSeasonCommandValidator>();

            return services;
        }
    }
}
