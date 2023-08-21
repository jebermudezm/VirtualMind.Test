
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.Library.Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> Get();
        Task<User> GetById(int id);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
       
    }
}
