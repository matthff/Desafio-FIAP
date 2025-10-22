using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

// TODO: Criar métodos específicos de persistência para Turma
public interface ITurmaRepository : IBaseRepository<Turma>
{
    Task<IEnumerable<Turma>> ObterTodosComQuantidadeDeAlunos();
}
