using VirtualMind.Test.Contracts.ServiceLibrary;
using VirtualMind.Test.Library.Contracts;
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.Impl.ServiceLibrary
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Create(User user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<IQueryable<User>> Get()
        {
            return await _userRepository.Get();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<bool> Update(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}