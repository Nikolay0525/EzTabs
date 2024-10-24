﻿using Microsoft.EntityFrameworkCore;

namespace EzTabs.Generic.Repository
{
    public class RepoImplementation<T> : IRepo<T> where T : class 
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        private RepoImplementation(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public static async Task<RepoImplementation<T>> CreateRepoAsync<YourDbContext>() where YourDbContext : DbContext, new()
        {
            var context = new YourDbContext();
            await context.Database.EnsureCreatedAsync();

            return new RepoImplementation<T>(context);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
       
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

    }
}
