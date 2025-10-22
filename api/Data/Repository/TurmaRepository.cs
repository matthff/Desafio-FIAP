using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class TurmaRepository : BaseRepository<Turma>, ITurmaRepository
{
    public TurmaRepository(ContextoDeDados context) : base(context) { }

    public async Task<IEnumerable<Turma>> ObterTodosComQuantidadeDeAlunos()
    {
        try
        {
            return await _dataset.Include(t => t.Alunos).OrderBy(t => t.Nome).ToListAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}

