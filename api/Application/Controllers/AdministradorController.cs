using System.Net;
using Api.Domain.DTO.Administrador;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Gerenciamento de administradores - V1
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Administrador")]
public class AdministradorController : ControllerBase
{
    private readonly IAdministradorService _administradorService;

    public AdministradorController(IAdministradorService administradorService)
    {
        _administradorService = administradorService;
    }

    /// <summary>
    /// Cadastra um novo administrador.
    /// </summary>
    /// <param name="administrador">Dados do administrador a ser cadastrado.</param>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InserirAdministrador([FromBody] AdministradorInserirDto administrador)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _administradorService.InserirAdministradorAsync(administrador);
            if (result != null)
            {
                return Created();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Atualiza um administrador.
    /// </summary>
    /// <param name="administrador">Dados do administrador a ser atualizado.</param>
    /// <remarks>
    /// Retorna um objeto com as informações sobre o administrador atualizado.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarAdministrador([FromBody] AdministradorAtualizarDto administrador)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _administradorService.AtualizarAdministradorAsync(administrador);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
