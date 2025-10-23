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
}
