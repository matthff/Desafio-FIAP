using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class MatriculaRepository : IMatriculaRepository
{
    protected readonly ContextoDeDados _context;
    protected readonly DbSet<Matricula> _dataset;

    public MatriculaRepository(ContextoDeDados context)
    {
        _context = context;
        _dataset = context.Set<Matricula>();
    }

    public async Task<IEnumerable<Matricula>> ObterMatriculasPorTurmaAsync(int turmaId)
    {
        return await _dataset
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .Where(m => m.TurmaId.Equals(turmaId))
            .ToListAsync();
    }

    public async Task<Matricula> InserirMatriculaAsync(Matricula matricula)
    {
        _dataset.Add(matricula);

        await _context.SaveChangesAsync();

        return matricula;
    }

    public Task<bool> ExisteAlunoMatriculadoNaTurmaAsync(int alunoId, int turmaId)
    {
        return _dataset.AnyAsync(m => m.AlunoId.Equals(alunoId) && m.TurmaId.Equals(turmaId));
    }

    public async Task<bool> ExcluirAsync(int matriculaId)
    {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(matriculaId));
        if (result == null)
            return false;

        _dataset.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }
}

