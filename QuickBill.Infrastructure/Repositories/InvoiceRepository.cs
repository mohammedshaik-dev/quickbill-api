using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;
using Dapper;

namespace QuickBill.Infrastructure
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly NpgsqlConnection _connection;

        public InvoiceRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<InvoiceDto>> GetAllAsync(Guid userId)
        {
            return (await _connection.QueryAsync<InvoiceDto>(InvoiceQueries.GetAll, new { UserId = userId })).ToList();
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            return await _connection.QueryFirstOrDefaultAsync<InvoiceDto>(InvoiceQueries.GetById, new { Id = id });
        }

        public async Task<Guid> CreateAsync(InvoiceDto dto)
        {
            dto.InvoiceNumber = await GenerateInvoiceNumberAsync(dto.UserId);
            return await _connection.ExecuteScalarAsync<Guid>(InvoiceQueries.Insert, dto);
        }

        public async Task<bool> UpdateAsync(InvoiceDto dto)
        {
            var rows = await _connection.ExecuteAsync(InvoiceQueries.Update, dto);
            return rows > 0;
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(InvoiceQueries.SoftDelete, new { Id = id });
            return rows > 0;
        }

        private async Task<string> GenerateInvoiceNumberAsync(Guid userId)
        {
            var sql = "SELECT COUNT(*) FROM invoices WHERE UserId = @UserId";
            int count = await _connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
            return $"INV-{DateTime.Now:yyyy}-{count + 1:D4}";
        }
    }
}
