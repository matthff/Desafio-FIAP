using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface IAlunoRepository : IBaseRepository<Aluno>
{
    Task<Aluno> ObterPorIdComNomeAsync(string alunoNome);

    Task<Aluno> ObterPorIdComCpfAsync(string alunoCpf);

    Task<bool> ExisteAlunoAsync(Aluno aluno);
}
