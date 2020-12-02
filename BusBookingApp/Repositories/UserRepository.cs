using BusBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.Repositories
{
    public class UserRepository : GeneralRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User Get(string Id)
        {
            var user = _dbContext.Users.Find(Id);
            return user;
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<bool> Update(User user)
        {
            var userToUpdate = _dbContext.Users.Find(user.Id);
            if (userToUpdate == null) throw new Exception("User not found to update.");
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.StudentId = user.StudentId;
            userToUpdate.PhoneNumber = user.PhoneNumber;

            return await SaveChangesAsync();
        }

    }
}
