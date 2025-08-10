using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.SqlQueries;

namespace QuickBill.Infrastructure
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<PaymentDetailsRepository> _logger;

        public PaymentDetailsRepository(NpgsqlConnection connection, ILogger<PaymentDetailsRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<PaymentDetailsDto>> GetAllByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("Fetching all payment details for user {UserId}", userId);
            return await _connection.QueryAsync<PaymentDetailsDto>(PaymentDetailsQueries.GetAllPaymentDetailsByUserId, new { UserId = userId });
        }

        public async Task<PaymentDetailsDto?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching payment details with ID {PaymentDetailsId}", id);
            return await _connection.QueryFirstOrDefaultAsync<PaymentDetailsDto>(PaymentDetailsQueries.GetPaymentDetailsById, new { Id = id });
        }

        public async Task<Guid> CreateAsync(PaymentDetailsDto dto)
            => await _connection.ExecuteScalarAsync<Guid>(PaymentDetailsQueries.InsertPaymentDetails, dto);

        public async Task<bool> UpdateAsync(PaymentDetailsDto dto)
            => (await _connection.ExecuteAsync(PaymentDetailsQueries.UpdatePaymentDetails, dto)) > 0;

        public async Task<bool> DeleteAsync(Guid id)
            => (await _connection.ExecuteAsync(PaymentDetailsQueries.SoftDeletePaymentDetails, new { Id = id })) > 0;
    }
} 