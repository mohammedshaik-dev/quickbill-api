using Dapper;
using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;


namespace QuickBill.Infrastructure
{
    public class ClientRepository : IClientRepository
    {
        private readonly NpgsqlConnection _connection;

        public ClientRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync(Guid userId)
       => await _connection.QueryAsync<ClientDto>(ClientQueries.GetAll, new { UserId = userId });

        public async Task<ClientDto?> GetByIdAsync(Guid id)
            => await _connection.QueryFirstOrDefaultAsync<ClientDto>(ClientQueries.GetById, new { Id = id });

        public async Task<Guid> CreateAsync(ClientDto dto)
            => await _connection.ExecuteScalarAsync<Guid>(ClientQueries.Insert, dto);

        public async Task<bool> UpdateAsync(ClientDto dto)
            => (await _connection.ExecuteAsync(ClientQueries.Update, dto)) > 0;

        public async Task<bool> DeleteAsync(Guid id)
            => (await _connection.ExecuteAsync(ClientQueries.Delete, new { Id = id })) > 0;
    }
}
