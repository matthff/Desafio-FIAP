using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO.Turma;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services
{
    public class TurmaService : BaseService<Turma, TurmaDto>, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMapper _mapper;

        public TurmaService(ITurmaRepository turmaRepository, IMapper mapper) : base(turmaRepository, mapper)
        {
            _turmaRepository = turmaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TurmaDto>> ObterTodosComQuantidadeDeAlunos()
        {
            var listEntity = await _turmaRepository.ObterTodosComQuantidadeDeAlunos();
            return _mapper.Map<IEnumerable<TurmaDto>>(listEntity);
        }

        public async Task<TurmaDto> Inserir(TurmaCreateDto trainerCreated)
        {
            var entity = _mapper.Map<Turma>(trainerCreated);
            var result = await _turmaRepository.InserirAsync(entity);

            return _mapper.Map<TurmaDto>(result);
        }
    }
}
