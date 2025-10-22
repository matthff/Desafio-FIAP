using Api.Data.Context;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;

namespace Api.Data.Repository
{
    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(ContextoDeDados context) : base(context)
        {

        }
    }
}
