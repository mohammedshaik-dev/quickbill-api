using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;
using Dapper;


namespace QuickBill.Infrastructure
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly NpgsqlConnection _connection;

        public InvoiceItemRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(Guid invoiceId)
        {
            return (await _connection.QueryAsync<InvoiceItemDto>(InvoiceItemQueries.GetAllByInvoiceId, new { InvoiceId = invoiceId })).ToList();
        }

        public async Task<InvoiceItemDto?> GetByIdAsync(Guid id)
        {
            return await _connection.QueryFirstOrDefaultAsync<InvoiceItemDto>(InvoiceItemQueries.GetById, new { Id = id });
        }

        public async Task<Guid> CreateAsync(InvoiceItemDto dto)
        {
            dto.Total = dto.Quantity * dto.UnitPrice;
            return await _connection.ExecuteScalarAsync<Guid>(InvoiceItemQueries.Insert, dto);
        }

        public async Task<bool> UpdateAsync(InvoiceItemDto dto)
        {
            dto.Total = dto.Quantity * dto.UnitPrice;
            var rows = await _connection.ExecuteAsync(InvoiceItemQueries.Update, dto);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _connection.ExecuteAsync(InvoiceItemQueries.Delete, new { Id = id });
            return rows > 0;
        }
    }
}
