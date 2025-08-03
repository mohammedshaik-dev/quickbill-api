using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure
{
    public class InvoicePdfRepository : IInvoicePdfRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<InvoicePdfRepository> _logger;

        public InvoicePdfRepository(NpgsqlConnection connection, ILogger<InvoicePdfRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<InvoicePdfDto?> GetInvoicePdfDataAsync(Guid invoiceId)
        {
            try
            {
                var invoice = await _connection.QueryFirstOrDefaultAsync<InvoicePdfDto>(
                    InvoicePdfQueries.GetInvoiceWithClient,
                    new { InvoiceId = invoiceId }
                );

                if (invoice != null)
                {
                    var items = await _connection.QueryAsync<InvoiceItemDto>(
                        InvoicePdfQueries.GetInvoiceItemsByInvoiceId,
                        new { InvoiceId = invoiceId }
                    );

                    invoice.Items = items.ToList();
                }

                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch invoice PDF data");
                return null;
            }
        }
    }
}
