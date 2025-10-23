using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;
using Domain.Utils;

namespace Api.Domain.Interfaces.Services;

public interface IAlunoService : IBaseService<AlunoDto>
{
    Task<PagedResult<AlunoDto>> ObterTodosOrdenadosPorNomeAsync(int page, int pageSize);

    Task<AlunoDto> ObterPorIdComNomeAsync(string alunoNome);

    Task<AlunoDto> ObterPorIdComCpfAsync(string alunoCpf);

    Task<AlunoDto> InserirAlunoAsync(AlunoInserirDto alunoCriado);

    Task<AlunoDto> AtualizarAlunoAsync(AlunoAtualizarDto alunoAtualizado);

    Task<bool> ExcluirAlunoAsync(int alunoId);
}

