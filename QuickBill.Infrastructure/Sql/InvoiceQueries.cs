
namespace QuickBill.Infrastructure.Sql
{
    public static class InvoiceQueries
    {
        public const string GetAll = @"
        SELECT id, UserId AS UserId, ClientId AS ClientId, InvoiceNumber AS InvoiceNumber,
               DateIssued AS DateIssued, DueDate AS DueDate, TotalAmount AS TotalAmount,
               Status AS Status, Notes AS Notes, IsDeleted AS IsDeleted, CreatedAt AS CreatedAt
        FROM invoices
        WHERE UserId = @UserId AND IsDeleted = FALSE
        ORDER BY DateIssued DESC";

        public const string GetById = @"
        SELECT id, UserId AS UserId, ClientId AS ClientId, InvoiceNumber AS InvoiceNumber,
               DateIssued AS DateIssued, DueDate AS DueDate, TotalAmount AS TotalAmount,
               Status AS Status, Notes AS Notes, IsDeleted AS IsDeleted, CreatedAt AS CreatedAt
        FROM invoices
        WHERE id = @Id AND IsDeleted = FALSE";

        public const string Insert = @"
        INSERT INTO invoices (UserId, ClientId, InvoiceNumber, DateIssued, DueDate,
                              TotalAmount, Status, Notes, CreatedAt)
        VALUES (@UserId, @ClientId, @InvoiceNumber, @DateIssued, @DueDate,
                @TotalAmount, @Status, @Notes, NOW())
        RETURNING id;";

        public const string Update = @"
        UPDATE invoices SET
            DateIssued = @DateIssued,
            DueDate = @DueDate,
            TotalAmount = @TotalAmount,
            Status = @Status,
            Notes = @Notes
        WHERE id = @Id AND UserId = @UserId AND IsDeleted = FALSE;";

        public const string SoftDelete = @"
        UPDATE invoices SET IsDeleted = TRUE WHERE id = @Id;";
    }
}
