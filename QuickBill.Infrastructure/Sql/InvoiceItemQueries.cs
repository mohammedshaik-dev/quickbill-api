
namespace QuickBill.Infrastructure.Sql
{
    public static class InvoiceItemQueries
    {
        public const string GetAllByInvoiceId = @"
        SELECT 
            Id, 
            InvoiceId, 
            Description, 
            Quantity, 
            UnitPrice, 
            Total
        FROM InvoiceItems
        WHERE InvoiceId = @InvoiceId";

        public const string GetById = @"
        SELECT 
            Id, 
            InvoiceId, 
            Description, 
            Quantity, 
            UnitPrice, 
            Total
        FROM InvoiceItems
        WHERE Id = @Id";

        public const string Insert = @"
        INSERT INTO InvoiceItems (InvoiceId, Description, Quantity, UnitPrice, Total)
        VALUES (@InvoiceId, @Description, @Quantity, @UnitPrice, @Total)
        RETURNING Id;";

        public const string Update = @"
        UPDATE InvoiceItems SET
            Description = @Description,
            Quantity = @Quantity,
            UnitPrice = @UnitPrice,
            Total = @Total
        WHERE Id = @Id;";

        public const string Delete = @"
        DELETE FROM InvoiceItems WHERE Id = @Id;";
    }
}
