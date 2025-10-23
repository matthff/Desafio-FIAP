using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;
using Api.Domain.DTO.Matricula;
using Api.Domain.DTO.Turma;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public MatriculaService(IMatriculaRepository matriculaRepository, IMapper mapper, ITurmaRepository turmaRepository)
    {
        _matriculaRepository = matriculaRepository;
        _mapper = mapper;

        _turmaRepository = turmaRepository;
    }

    public async Task MatricularAlunoAsync(MatriculaDoAlunoDto matriculaDoAluno)
    {
        int alunoId = matriculaDoAluno.AlunoId;
        int turmaId = matriculaDoAluno.TurmaId;

        if (await _matriculaRepository.ExisteAlunoMatriculadoNaTurmaAsync(alunoId, turmaId))
            return;

        var matricula = new Matricula(alunoId, turmaId);

        await _matriculaRepository.InserirMatriculaAsync(matricula);
    }

    public async Task<MatriculasDaTurmaDto> ObterMatriculasDaTurmaAsync(int turmaId)
    {
        var turma = await _turmaRepository.ObterPorIdAsync(turmaId);
        var matriculas = await _matriculaRepository.ObterMatriculasPorTurmaAsync(turmaId);

        var alunosMatriculados = matriculas.Select(m => new AlunoMatriculadoDto
        {
            Aluno = _mapper.Map<AlunoDto>(m.Aluno),
            DataMatricula = m.DataMatricula
        });

        var resultado = new MatriculasDaTurmaDto
        {
            NomeDaTurma = turma.Nome,
            AlunosMatriculados = alunosMatriculados,
        };

        return resultado;
    }

    public async Task<bool> ExcluirMatriculaAsync(int matriculaId)
    {
        return await _matriculaRepository.ExcluirAsync(matriculaId);
    }
}
