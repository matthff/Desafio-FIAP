using Api.Domain.DTO;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Dependecies
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAlunoService, AlunoService>();
            serviceCollection.AddScoped<ITurmaService, TurmaService>();
            serviceCollection.AddScoped<IMatriculaService, MatriculaService>();
            serviceCollection.AddTransient(typeof(ISenhaService<>), typeof(SenhaService<>));
        }
    }
}
