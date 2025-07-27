using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure;

namespace QuickBill.Application
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ClientDto>> GetAllAsync(Guid userId) => _repository.GetAllAsync(userId);
        public Task<ClientDto?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task<Guid> CreateAsync(ClientDto dto) => _repository.CreateAsync(dto);
        public Task<bool> UpdateAsync(ClientDto dto) => _repository.UpdateAsync(dto);
        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
