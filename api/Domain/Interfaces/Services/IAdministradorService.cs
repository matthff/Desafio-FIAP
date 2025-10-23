using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;

namespace Api.Domain.Interfaces.Services;

public interface IAdministradorService
{
    Task<AdministradorDto> InserirAdministrador(AdministradorInserirDto administradorCriado);

    Task<AdministradorDto> AtualizarAdministrador(AdministradorAtualizarDto administradorAtualizado);
}

