using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public interface IGeneralRepository
    {
        void Add<T>(T entity) where T : class;
        Task<T> GetAsync<T>(int entityId) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<bool> UpdateAsync<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
