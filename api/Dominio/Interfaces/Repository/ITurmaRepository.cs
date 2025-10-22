using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dominio.Entidades;

namespace Api.Dominio.Interfaces.Repository;

// TODO: Criar métodos específicos de persistência para Turma
public interface ITurmaRepository : IBaseRepository<Turma>
{
    Task<IEnumerable<Turma>> ObterTodosComQuantidadeDeAlunos();
}
