using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure;



namespace QuickBill.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<UserDto?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

        public async Task<Guid> CreateAsync(UserDto user) => await _repository.CreateAsync(user);

        public async Task<bool> UpdateAsync(UserDto user) => await _repository.UpdateAsync(user);

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
