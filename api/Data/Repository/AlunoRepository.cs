using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    public AlunoRepository(ContextoDeDados context) : base(context) { }

    public async Task<Aluno> ObterPorIdComNome(string alunoNome)
    {
        return await _dataset.SingleOrDefaultAsync(p => p.Nome.Equals(alunoNome));
    }

    public async Task<Aluno> ObterPorIdComCpf(string alunoCpf)
    {
        return await _dataset.SingleOrDefaultAsync(p => p.Cpf.Equals(alunoCpf));
    }
}

