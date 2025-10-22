using System.Threading.Tasks;
using Api.Dominio.Entidades;

namespace Api.Dominio.Interfaces.Repository;

// TODO: Criar métodos específicos de persistência para Aluno
public interface IAlunoRepository : IBaseRepository<Aluno>
{
    //Task<Aluno> FindCompleteById(int id);
}
