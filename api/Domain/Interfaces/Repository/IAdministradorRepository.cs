using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface IAdministradorRepository : IBaseRepository<Administrador>
{
    Task<bool> ExisteAdministradorComMesmoEmailAsync(Administrador administrador);

    Task<Administrador> ObterAdministradorPorEmailAsync(string email);

    Task<Administrador> RecarregarInformacoesDoAdministradorAsync(Administrador administrador);

    Task<bool> RevogarTokenAsync(string email);
}
