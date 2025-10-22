using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dominio.DTO;

namespace Api.Dominio.Interfaces.Services;

public interface IBaseService<A>
    where A : BaseDto
{
    Task<IEnumerable<A>> ObterTodosAsync();

    Task<A> ObterPorIdAsync(int id);
}

