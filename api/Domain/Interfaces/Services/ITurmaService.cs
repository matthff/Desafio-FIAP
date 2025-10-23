using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Turma;
using Domain.Utils;

namespace Api.Domain.Interfaces.Services;

public interface ITurmaService : IBaseService<TurmaDto>
{
    Task<PagedResult<TurmaDto>> ObterTurmasOrdenadasPorNomeComQuantidadeDeAlunosAsync(int page, int pageSize);

    Task<TurmaDto> ObterPorIdComQuantidadeDeAlunosAsync(int turmaId);

    Task<TurmaDto> InserirTurmaAsync(TurmaInserirDto turmaCriada);

    Task<TurmaDto> AtualizarTurmaAsync(TurmaAtualizarDto turmaAtualizada);

    Task<bool> ExcluirTurmaAsync(int turmaId);
}

