using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Dominio.Entidades;
using Api.Dominio.Interfaces.Repository;
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
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<T> InserirAsync(T item)
        {
            try
            {
                item.DataCadastro = DateTime.UtcNow;
                _dataset.Add(item);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return item;
        }

        public async Task<T> AtualizarAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                    return null;

                item.DataAtualizacao = DateTime.UtcNow;
                item.DataCadastro = result.DataCadastro;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return item;
        }

        public async Task<T> AtualizarParcialAsync(
            T item,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                foreach (var includeProperty in includeProperties)
                {
                    _context.Entry(item).Property(includeProperty).IsModified = true;
                }

                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                    return null;

                item.DataAtualizacao = DateTime.UtcNow;
                item.DataCadastro = result.DataCadastro;

                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return item;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                    return false;

                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }
    }
}
