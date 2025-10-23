using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Turma;
using Domain.Utils;

namespace Api.Domain.Interfaces.Services;

public interface ITurmaService : IBaseService<TurmaDto>
{
    Task<PagedResult<TurmaDto>> ObterTodosOrdenadosPorNomeComQuantidadeDeAlunos(int page, int pageSize);

    Task<TurmaDto> ObterPorIdComQuantidadeDeAlunos(int turmaId);

    Task<TurmaDto> InserirTurma(TurmaInserirDto turmaCriada);

    Task<TurmaDto> AtualizarTurma(TurmaAtualizarDto turmaAtualizada);

    Task<bool> ExcluirTurma(int turmaId);
}

