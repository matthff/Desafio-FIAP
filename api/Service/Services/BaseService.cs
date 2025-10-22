using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dominio.DTO;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;
using Api.Dominio.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services
{
    public class BaseService<T, A> : IBaseService<A>
        where T : EntidadeBase
        where A : BaseDto
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<A>> ObterTodosAsync()
        {
            var listEntity = await _repository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<A>>(listEntity);
        }

        public async Task<A> ObterPorIdAsync(int id)
        {
            return _mapper.Map<A>(await _repository.ObterPorIdAsync(id));
        }
    }
}
