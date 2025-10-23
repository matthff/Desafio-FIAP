using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class AdministradorRepository : BaseRepository<Administrador>, IAdministradorRepository
{
    public AdministradorRepository(ContextoDeDados context) : base(context) { }

    public async Task<bool> ExisteAdministradorComMesmoEmailAsync(Administrador administrador)
    {
        return await _dataset.AnyAsync(p => p.Email.Equals(administrador.Email));
    }

    public async Task<Administrador> ObterAdministradorPorEmail(string email)
    {
        return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<Administrador> RecarregarInformacoesDoAdministrador(Administrador administrador)
    {
        if (!await _dataset.AnyAsync(u => u.Id.Equals(administrador.Id)))
            return null;

        var result = await ObterPorIdAsync(administrador.Id);

        if (result != null)
        {
            _context.Entry(result).CurrentValues.SetValues(administrador);
            await _context.SaveChangesAsync();
        }

        return result;
    }

    public async Task<bool> RevogarToken(string email)
    {
        var administrador = await ObterAdministradorPorEmail(email);
        if (administrador is null)
            return false;

        administrador.RevogarRefreshToken();
        await _context.SaveChangesAsync();

        return true;
    }
}
