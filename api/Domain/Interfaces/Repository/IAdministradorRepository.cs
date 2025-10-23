using System.Threading.Tasks;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Repository;

public interface IAdministradorRepository : IBaseRepository<Administrador>
{
    Task<bool> ExisteAdministradorComMesmoEmailAsync(Administrador administrador);

    Task<Administrador> ObterAdministradorPorEmail(string email);

    Task<Administrador> RecarregarInformacoesDoAdministrador(Administrador administrador);

    Task<bool> RevogarToken(string email);
}
