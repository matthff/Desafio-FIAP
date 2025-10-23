using System.Net;
using Api.Domain.DTO.Administrador;
using Api.Domain.DTO.Token;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers;

/// <summary>
/// Gerenciamento de logins - V1
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// Realiza login.
    /// </summary>
    /// <remarks>
    /// Retorna as informações do token caso o login seja válido.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost]
    [Route("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignIn([FromBody] AdministradorLoginDto administradorLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (administradorLoginDto == null)
                return BadRequest("Credenciais inválidas");

            var token = await _loginService.ValidarLogin(administradorLoginDto);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Recarrega o token JWT.
    /// </summary>
    /// <remarks>
    /// Recarrega o token JWT utilizando o token de atualização (refresh token) válido.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost]
    [Route("recarregarToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RecarregarToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (refreshTokenDto is null)
                return BadRequest("Requisição está inválida");

            var token = await _loginService.ValidarLoginComTokenERefreshToken(refreshTokenDto);
            if (token == null)
                return BadRequest("Token ou RefreshToken inválidos");

            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Revoga o token JWT.
    /// </summary>
    [Authorize("Bearer")]
    [HttpDelete]
    [Route("revogarToken")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevogarToken()
    {
        var email = User?.Identity?.Name;
        var result = await _loginService.RevogarToken(email);

        if (!result)
            return Unauthorized("Não foi possível revogar o token com a conta atualmente autenticada.");

        return NoContent();
    }
}

