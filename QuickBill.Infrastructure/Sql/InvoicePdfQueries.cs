using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure.Sql
{
    public static class InvoicePdfQueries
    {
        public const string GetInvoiceWithClient =
        @"SELECT i.id AS InvoiceId, i.invoicenumber AS InvoiceNumber, i.dateissued AS DateIssued,
                 i.duedate AS DueDate, i.totalamount AS TotalAmount, i.notes AS Notes,
                 c.name AS ClientName, c.email AS ClientEmail, c.phonenumber AS ClientPhone, c.address AS ClientAddress
          FROM invoices i
          JOIN clients c ON i.clientid = c.id
          WHERE i.id = @InvoiceId AND i.isdeleted = FALSE";

        public const string GetInvoiceItemsByInvoiceId =
        @"SELECT Id, InvoiceId, description, quantity, UnitPrice, Total
          FROM InvoiceItems
          WHERE InvoiceId = @InvoiceId";
    }
}
