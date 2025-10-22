using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : EntidadeBase
    {
        protected readonly ContextoDeDados _context;
        protected readonly DbSet<T> _dataset;

        public BaseRepository(ContextoDeDados context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InserirAsync(T item)
        {
            item.DataCadastro = DateTime.UtcNow;
            item.Ativo = true;
            _dataset.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> AtualizarAsync(T item)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;

            item.DataAtualizacao = DateTime.UtcNow;
            item.DataCadastro = result.DataCadastro;

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> AtualizarParcialAsync(
            T item,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var result = await _dataset.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;

            foreach (var includeProperty in includeProperties)
            {
                _context.Entry(item).Property(includeProperty).IsModified = true;
            }

            item.Ativo = result.Ativo;
            item.DataAtualizacao = DateTime.UtcNow;
            item.DataCadastro = result.DataCadastro;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }
    }
}
