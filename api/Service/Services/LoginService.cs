using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.DTO.Token;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;

namespace Api.Service.Services;

public class LoginService : ILoginService
{
    private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

    private readonly IAdministradorService _administradorService;
    private readonly TokenConfiguration _configuration;
    private readonly IAuthenticationService _authenticationService;

    public LoginService(IAdministradorService administradorService,
        TokenConfiguration configuration,
        IAuthenticationService authenticationService)
    {
        _administradorService = administradorService;
        _configuration = configuration;
        _authenticationService = authenticationService;
    }

    public async Task<TokenDto> ValidarLoginAsync(AdministradorLoginDto administradorLoginDto)
    {
        var administrador = await _administradorService.ValidarCredenciaisDoAdministradorAsync(administradorLoginDto);

        if (administrador == null)
            return null;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, administradorLoginDto.Email)
        };

        var accessToken = _authenticationService.GenerateAccessToken(claims);
        var refreshToken = _authenticationService.GenerateRefreshToken();

        administrador.DefinirRefreshToken(refreshToken);
        administrador.DefinirRefreshTokenExpireTime(DateTime.Now.AddDays(_configuration.DaysToExpire));

        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        await _administradorService.RecarregarInformacoesDoAdministradorAsync(administrador);

        return new TokenDto
        {
            Authenticated = true,
            CreateDate = createDate.ToString(DATE_FORMAT),
            ExpirationDate = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenDto> ValidarLoginComTokenERefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var accessToken = refreshTokenDto.AccessToken;
        var refreshToken = refreshTokenDto.RefreshToken;

        var principal = _authenticationService.GetPrincipalFromExpiredToken(accessToken);

        var administradorEmail = principal.Identity.Name;

        var administrador = await _administradorService.ObterAdministradorPorEmailAsync(administradorEmail);

        if (administrador == null ||
            administrador.RefreshToken != refreshToken ||
            administrador.RefreshTokenExpireTime <= DateTime.Now) return null;

        accessToken = _authenticationService.GenerateAccessToken(principal.Claims);
        refreshToken = _authenticationService.GenerateRefreshToken();

        administrador.DefinirRefreshToken(refreshToken);

        await _administradorService.RecarregarInformacoesDoAdministradorAsync(administrador);

        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        return new TokenDto
        {
            Authenticated = true,
            CreateDate = createDate.ToString(DATE_FORMAT),
            ExpirationDate = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<bool> RevogarTokenAsync(string email)
    {
        return await _administradorService.RevogarTokenAsync(email);
    }
}

