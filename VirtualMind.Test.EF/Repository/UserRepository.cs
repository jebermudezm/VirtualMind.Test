using Microsoft.EntityFrameworkCore;
using System.Linq;
using VirtualMind.Test.EF.Context;
using VirtualMind.Test.Library.Contracts;
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.EF.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }
        

        public async Task<bool> Create(User user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return true;
        }

       

        public async  Task<bool> Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                await Task.Run(() => _context.Users.Remove(user)).ConfigureAwait(false);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<User>> Get()
        {
            return await Task.Run(() => _context.Users.AsQueryable()).ConfigureAwait(false); 
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id) ?? new User();
        }

        public async Task<bool> Update(User user)
        {
            await Task.Run(() => _context.Entry(user).State = EntityState.Modified).ConfigureAwait(false); 
            return true;
        }

    }
}
