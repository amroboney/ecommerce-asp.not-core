using System;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected DbSet<T> dbSet;
        

        // constructor will take the context and logger factory as parameters
        public GenericRepository(DataContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All() 
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Error getting entity with id {Id}", id);
                return null;
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Error adding entity");
                return false;
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            try
            {
                var entity = await dbSet.FindAsync(id);
                if (entity != null)
                {
                    dbSet.Remove(entity);
                    return true;
                }
                else
                {
                    //_logger.LogWarning("Entity with id {Id} not found for deletion", id);
                    return false;
                }
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Error deleting entity with id {Id}", id);
                return false;
            }
        }

        public virtual async Task<bool> Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
   
}

