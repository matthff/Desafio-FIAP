using Api.Dominio.DTO;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Services;
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
        }
    }
}
