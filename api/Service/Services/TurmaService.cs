using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dominio.DTO.Turma;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;
using Api.Dominio.Interfaces.Services;
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
    }
}
