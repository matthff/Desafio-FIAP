using System.Net;
using Api.Domain.DTO.Turma;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Gerenciamento de turmas - V1
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Turma")]
public class TurmaController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmaController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    /// <summary>
    /// Lista todas as turmas.
    /// </summary>
    /// <remarks>
    /// Retorna uma lista paginada ordenada por ordem alfabética pelo nome de todas as turmas cadastradas no sistema.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterTurmasComQuantidadeDeAlunos([FromQuery] int page)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.ObterTodosOrdenadosPorNomeComQuantidadeDeAlunos(page, Pagination.DefaultPageSize);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Obter uma turma pelo seu identificador.
    /// </summary>
    /// <remarks>
    /// Retorna um objeto com as informações sobre a turma e sua quantidade de alunos.
    /// </remarks>
    /// <param name="turmaId">Identificador da turma.</param>
    [Authorize("Bearer")]
    [HttpGet("{turmaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterTurmaPorIdComQuantidadeDeAlunos(int turmaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.ObterPorIdComQuantidadeDeAlunos(turmaId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Cadastra uma nova turma.
    /// </summary>
    /// <param name="turma">Dados da turma a ser cadastrada.</param>
    [Authorize("Bearer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InserirTurma([FromBody] TurmaInserirDto turma)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.InserirTurma(turma);
            if (result != null)
            {
                return Created();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Atualiza uma turma.
    /// </summary>
    /// <param name="turma">Dados da turma a ser atualizada.</param>
    /// <remarks>
    /// Retorna um objeto com as informações sobre a turma atualizada.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarTurma([FromBody] TurmaAtualizarDto turma)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.AtualizarTurma(turma);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Exclui uma turma.
    /// </summary>
    /// <param name="turmaId">Identificador da turma.</param>
    /// <remarks>
    /// Retorna um um booleano representando a exclusão da turma.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpDelete("{turmaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirTurma(int turmaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.ExcluirTurma(turmaId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
