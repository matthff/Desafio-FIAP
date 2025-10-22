using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface IAlunoRepository : IBaseRepository<Aluno>
{
    Task<Aluno> ObterPorIdComNome(string alunoNome);

    Task<Aluno> ObterPorIdComCpf(string alunoCpf);

    Task<bool> ExisteAluno(Aluno aluno);
}
