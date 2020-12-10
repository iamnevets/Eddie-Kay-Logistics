using BusBookingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
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
            Console.WriteLine($"Adding an object of type {entity.GetType()} to the context.");
            _dbContext.Add(entity);
        }

        public async Task<bool> Update<T>(T entity) where T : class
        {
            Console.WriteLine($"Updating an object of type {entity.GetType()} to the context.");
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public void Delete<T>(T entity) where T : class
        {
            Console.WriteLine($"Removing an object of type {entity.GetType()} from the context.");
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
