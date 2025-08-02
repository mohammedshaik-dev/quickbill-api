
using Dapper;
using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;


namespace QuickBill.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection;

        public UserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _connection.QueryAsync<UserDto>(UserQueries.GetAll);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            return await _connection.QueryFirstOrDefaultAsync<UserDto>(UserQueries.GetById, new { Id = id });
        }

        public async Task<Guid> CreateAsync(UserDto user)
        {
            return await _connection.ExecuteScalarAsync<Guid>(UserQueries.Insert, user);
        }

        public async Task<bool> UpdateAsync(UserDto user)
        {
            var rows = await _connection.ExecuteAsync(UserQueries.Update, user);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(UserQueries.SoftDelete, new { Id = id });
            return rows > 0;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var sql = UserQueries.GetByEmail;
            var param = new { Email = email };
            return await _connection.QueryFirstOrDefaultAsync<UserDto>(sql, param);
        }
    }
}
