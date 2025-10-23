using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Services;

public interface IAdministradorService
{
    Task<Administrador> ObterAdministradorPorEmail(string email);

    Task<AdministradorDto> InserirAdministrador(AdministradorInserirDto administradorCriado);

    Task<AdministradorDto> AtualizarAdministrador(AdministradorAtualizarDto administradorAtualizado);

    Task<Administrador> ValidarCredenciaisDoAdministrador(AdministradorLoginDto administradorLogin);

    Task RecarregarInformacoesDoAdministrador(Administrador administrador);

    Task<bool> RevogarToken(string email);
}

