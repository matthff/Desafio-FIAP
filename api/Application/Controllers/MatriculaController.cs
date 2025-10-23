using System.Net;
using Api.Domain.DTO.Turma;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Gerenciamento de matrículas - V1
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Matricula")]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;

    public MatriculaController(IMatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    /// <summary>
    /// Lista todas as matriculas de uma turma.
    /// </summary>
    /// <param name="turmaId">Identificador da turma.</param>
    /// <remarks>
    /// Retorna uma lista paginada de todas as matriculas de uma turma com seus respectivos alunos e com as datas das matrículas.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpGet("{turmaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterMatriculasDaTurma(int turmaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _matriculaService.ObterMatriculasDaTurmaAsync(turmaId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Matricular um aluno em uma turma.
    /// </summary>
    /// <param name="matriculaDoAluno">Objeto com o identificador de aluno e de turma para realizar a matrícula.</param>
    [Authorize("Bearer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MatricularAluno([FromBody] MatriculaDoAlunoDto matriculaDoAluno)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _matriculaService.MatricularAlunoAsync(matriculaDoAluno);

            return Created();
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Exclui uma matrícula.
    /// </summary>
    /// <param name="matriculaId">Identificador da matricula.</param>
    /// <remarks>
    /// Retorna um um booleano representando a exclusão da matricula.
    /// </remarks>
    [Authorize("Bearer")]
    [HttpDelete("{matriculaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirTurma(int matriculaId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _matriculaService.ExcluirMatriculaAsync(matriculaId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
