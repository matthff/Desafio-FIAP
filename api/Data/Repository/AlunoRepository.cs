using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    public AlunoRepository(ContextoDeDados context) : base(context) { }

    public async Task<Aluno> ObterPorIdComNomeAsync(string alunoNome)
    {
        return await _dataset.SingleOrDefaultAsync(p => p.Nome.Equals(alunoNome));
    }

    public async Task<Aluno> ObterPorIdComCpfAsync(string alunoCpf)
    {
        return await _dataset.SingleOrDefaultAsync(p => p.Cpf.Equals(alunoCpf));
    }

    public async Task<bool> ExisteAlunoAsync(Aluno aluno)
    {
        return await _dataset.AnyAsync(p => p.Cpf.Equals(aluno.Cpf) || p.Email.Equals(aluno.Email));
    }
}

