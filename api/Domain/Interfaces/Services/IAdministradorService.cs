using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.Entidades;

namespace Api.Domain.Interfaces.Services;

public interface IAdministradorService
{
    Task<Administrador> ObterAdministradorPorEmailAsync(string email);

    Task<AdministradorDto> InserirAdministradorAsync(AdministradorInserirDto administradorCriado);

    Task<AdministradorDto> AtualizarAdministradorAsync(AdministradorAtualizarDto administradorAtualizado);

    Task<Administrador> ValidarCredenciaisDoAdministradorAsync(AdministradorLoginDto administradorLogin);

    Task RecarregarInformacoesDoAdministradorAsync(Administrador administrador);

    Task<bool> RevogarTokenAsync(string email);
}

