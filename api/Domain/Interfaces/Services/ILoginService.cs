using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.DTO.Token;

namespace Api.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<TokenDto> ValidarLogin(AdministradorLoginDto administradorLoginDto);
    Task<TokenDto> ValidarLoginComTokenERefreshToken(RefreshTokenDto refreshTokenDto);
    Task<bool> RevogarToken(string administradorEmail);
}

