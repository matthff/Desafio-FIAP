using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTO;

namespace Api.Domain.Interfaces.Services;

public interface IBaseService<A>
    where A : BaseDto
{
    Task<IEnumerable<A>> ObterTodosAsync();

    Task<A> ObterPorIdAsync(int id);
}

