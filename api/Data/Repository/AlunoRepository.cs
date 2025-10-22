using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;

namespace Api.Data.Repository
{
    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(ContextoDeDados context) : base(context)
        {

        }
    }
}
