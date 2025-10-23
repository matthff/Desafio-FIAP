using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.DTO.Token;

namespace Api.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<TokenDto> ValidarLoginAsync(AdministradorLoginDto administradorLoginDto);
    Task<TokenDto> ValidarLoginComTokenERefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<bool> RevogarTokenAsync(string administradorEmail);
}

