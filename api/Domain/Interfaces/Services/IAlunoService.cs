using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;
using Domain.Utils;

namespace Api.Domain.Interfaces.Services;

//TODO: Criar métodos específicos para Aluno
public interface IAlunoService : IBaseService<AlunoDto>
{
    Task<PagedResult<AlunoDto>> ObterTodosOrdenadosPorNome(int page, int pageSize);

    Task<AlunoDto> ObterPorIdComNome(string alunoNome);

    Task<AlunoDto> ObterPorIdComCpf(string alunoCpf);

    Task<AlunoDto> InserirAluno(AlunoInserirDto alunoCriado);

    Task<AlunoDto> AtualizarAluno(AlunoAtualizarDto alunoAtualizado);

    Task<bool> ExcluirAluno(int alunoId);
}

