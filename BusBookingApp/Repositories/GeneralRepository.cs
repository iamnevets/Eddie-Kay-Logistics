using BusBookingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GeneralRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            Console.WriteLine($"Adding an object of type {entity.GetType().Name} to the context.");
            _dbContext.Add(entity);
        }

        public async Task<T> GetAsync<T>(int entityId) where T : class
        {
            //Console.WriteLine($"Getting an object of type {entity.GetType().Name} to the context.");
            return await _dbContext.FindAsync<T>(entityId);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var dbContext = _dbContext.Set<T>();
            return await dbContext.AnyAsync() ? await dbContext.ToListAsync() : throw new Exception($"There are no {dbContext.EntityType.DisplayName()}.");
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            Console.WriteLine($"Updating an object of type {entity.GetType().Name} to the context.");
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);
            return await SaveChangesAsync();
        }

        public void Delete<T>(T entity) where T : class
        {
            Console.WriteLine($"Removing an object of type {entity.GetType().Name} from the context.");
            _dbContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            Console.WriteLine($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
