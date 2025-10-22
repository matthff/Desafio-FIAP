using Api.CrossCutting.DtoMapper.Aluno;
using Api.CrossCutting.DtoMapper.Turma;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Dependecies;

public static class ConfigureAutoMapper
{
    public static void ConfigureAutoMapperProfiles(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new AlunoProfile());
            cfg.AddProfile(new TurmaProfile());
        });
    }
}

