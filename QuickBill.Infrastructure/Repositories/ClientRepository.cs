using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;


namespace QuickBill.Infrastructure
{
    public class ClientRepository : IClientRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<ClientRepository>  _logger;

        public ClientRepository(NpgsqlConnection connection, ILogger<ClientRepository> logger )
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync(Guid userId)
        {
            _logger.LogInformation("Fetching all clients for user {UserId}", userId);
            return await _connection.QueryAsync<ClientDto>(ClientQueries.GetAll, new { UserId = userId });
        }

        public async Task<ClientDto?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching client with ID {ClientId}", id);   
            return await _connection.QueryFirstOrDefaultAsync<ClientDto>(ClientQueries.GetById, new { Id = id });
        }

        public async Task<Guid> CreateAsync(ClientDto dto)
            => await _connection.ExecuteScalarAsync<Guid>(ClientQueries.Insert, dto);

        public async Task<bool> UpdateAsync(ClientDto dto)
            => (await _connection.ExecuteAsync(ClientQueries.Update, dto)) > 0;

        public async Task<bool> DeleteAsync(Guid id)
            => (await _connection.ExecuteAsync(ClientQueries.Delete, new { Id = id })) > 0;
    }
}
