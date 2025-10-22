using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;

namespace Api.Domain.Interfaces.Services;

//TODO: Criar métodos específicos para Aluno
public interface IAlunoService : IBaseService<AlunoDto>
{
    Task<IEnumerable<AlunoDto>> ObterTodosAlunosOrdenadosPorNome();

    Task<AlunoDto> ObterPorIdComNome(string alunoNome);

    Task<AlunoDto> ObterPorIdComCpf(string alunoCpf);

    Task<AlunoDto> InserirAluno(AlunoInserirDto alunoCriado);

    Task<AlunoDto> AtualizarAluno(AlunoAtualizarDto alunoAtualizado);

    Task<bool> ExcluirAluno(int alunoId);
}

