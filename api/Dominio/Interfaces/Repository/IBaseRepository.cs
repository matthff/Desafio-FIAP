using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Dominio.Entidades;

namespace Api.Dominio.Interfaces.Repository
{
    public interface IBaseRepository<T>
        where T : EntidadeBase
    {
        Task<T> ObterPorIdAsync(int id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> InserirAsync(T item);
        Task<T> AtualizarAsync(T item);
        Task<T> AtualizarParcialAsync(T item, params Expression<Func<T, object>>[] includeProperties);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
