using EzTabs.Data.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EzTabs.Data.Repository
{
    public class RepoImplementation<T> : IRepo<T> where T : class 
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepoImplementation(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<OperationResult<IEnumerable<T>>> GetAll()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return new OperationResult<IEnumerable<T>>
                {
                    Success = true,
                    Data = entities,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<T>>
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> GetById(Guid id)
        {
            try
            {
                var item = await _dbSet.FindAsync(id);
                return new OperationResult<T>
                {
                    Success = true,
                    Data = item,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }
        
        public async Task<OperationResult<T>> GetByCompositeId(Guid firstKey, Guid secondKey)
        {
            try
            {
                var item = await _dbSet.FindAsync(firstKey, secondKey);
                return new OperationResult<T>
                {
                    Success = true,
                    Data = item,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<bool>> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var item = await _dbSet.AnyAsync(predicate);
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = item,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Add(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Update(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.UpdateRange(entities);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<OperationResult<T>> Delete(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync();
                return new OperationResult<T>
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
