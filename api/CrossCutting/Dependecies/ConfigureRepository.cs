using System;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Dependecies
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IAlunoRepository, AlunoRepository>();
            serviceCollection.AddScoped<ITurmaRepository, TurmaRepository>();
            serviceCollection.AddScoped<IMatriculaRepository, MatriculaRepository>();
            serviceCollection.AddScoped<IAdministradorRepository, AdministradorRepository>();

            var dbProvider = Environment.GetEnvironmentVariable("DB_PROVIDER") ?? "SQLSERVER";

            if (string.Equals(dbProvider, "SQLSERVER", StringComparison.OrdinalIgnoreCase))
            {
                // For local development (not in Docker)
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // For Docker deployment (when you containerize the API later)
                // var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

                serviceCollection.AddDbContext<ContextoDeDados>(
                    options => options.UseSqlServer(connectionString)
                );
            }
            else
            {
                //To use another type of database instead of SQLSERVER, you can define here (Test purposes)
            }
        }
    }
}
