using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

// TODO: Criar métodos específicos de persistência para Aluno
public interface IAlunoRepository : IBaseRepository<Aluno>
{
    //Task<Aluno> FindCompleteById(int id);
}
