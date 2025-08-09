using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuickBill.Domain.DTOs;

namespace QuickBill.Application.Document
{
    public class InvoiceDocument : IDocument
    {
        private readonly InvoicePdfDto _invoice;
        private readonly List<InvoiceItemDto> _items;
        private readonly string _logoPath;
        private readonly string _companyName;
        private readonly string _companyAddress;
        private readonly string _companyEmail;
        private readonly string _companyPhone;

        public InvoiceDocument(
            InvoicePdfDto invoice,
            List<InvoiceItemDto> items,
            string logoPath,
            string companyName,
            string companyAddress,
            string companyEmail,
            string companyPhone)
        {
            _invoice = invoice;
            _items = items;
            _logoPath = logoPath;
            _companyName = companyName;
            _companyAddress = companyAddress;
            _companyEmail = companyEmail;
            _companyPhone = companyPhone;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                // Left: Logo + Company Name
                row.ConstantItem(240).Column(col =>
                {
                    if (File.Exists(_logoPath))
                    {
                        var image = Image.FromFile(_logoPath);
                        col.Item().Height(50).Image(image).FitArea();
                    }

                    if (!string.IsNullOrWhiteSpace(_companyName))
                    {
                        col.Item().PaddingTop(6)
                           .Text(_companyName)
                           .FontSize(16)
                           .Bold()
                           .FontColor(Colors.Black);
                    }
                });

                // Right: Invoice title + basic info
                row.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().Text("INVOICE")
                        .FontSize(26)
                        .Bold()
                        .FontColor(Colors.Blue.Medium);

                    col.Item().PaddingTop(6).Column(info =>
                    {
                        info.Item().AlignRight().Text($"Invoice #: {_invoice.InvoiceNumber}");
                        info.Item().AlignRight().Text($"Issued: {_invoice.DateIssued:yyyy-MM-dd}");
                        if (_invoice.DueDate.HasValue)
                            info.Item().AlignRight().Text($"Due: {_invoice.DueDate:yyyy-MM-dd}");
                    });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(12);

                col.Item().Element(ComposeBillTo);

                // Separator before items
                col.Item().PaddingTop(8)
                           .BorderBottom(1)
                           .BorderColor(Colors.Grey.Lighten2);

                col.Item().Element(ComposeItemsTable);
                col.Item().Element(ComposeTotals);
                col.Item().Element(ComposeNotes);
            });
        }

        private void ComposeBillTo(IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(4);
                col.Item().Text("Bill To").Bold().FontSize(12);
                col.Item().Text(_invoice.ClientName).Bold();
                if (!string.IsNullOrWhiteSpace(_invoice.ClientAddress))
                    col.Item().Text(_invoice.ClientAddress);
                if (!string.IsNullOrWhiteSpace(_invoice.ClientEmail))
                    col.Item().Text(_invoice.ClientEmail);
            });
        }

        private void ComposeItemsTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);   // Item
                    columns.RelativeColumn(6);   // Description
                    columns.RelativeColumn(2);   // Quantity
                    columns.RelativeColumn(2);   // Unit Price
                    columns.RelativeColumn(2);   // Total
                });

                // Header
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(6).Text("Item").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(6).Text("Description").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Quantity").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Unit Price").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Total").Bold();
                });

                // Rows
                for (int i = 0; i < _items.Count; i++)
                {
                    var item = _items[i];

                    table.Cell().PaddingVertical(5).Text((i + 1).ToString());
                    table.Cell().PaddingVertical(5).Text(item.Description);
                    table.Cell().PaddingVertical(5).AlignRight().Text(item.Quantity.ToString());
                    table.Cell().PaddingVertical(5).AlignRight().Text($"{item.UnitPrice:C}");
                    table.Cell().PaddingVertical(5).AlignRight().Text($"{item.Total:C}");
                }

                // Bottom border
                table.Cell().ColumnSpan(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text("");
            });
        }

        private void ComposeTotals(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem();
                row.ConstantItem(240).Column(col =>
                {
                    col.Item().PaddingTop(8).Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text("Total:").Bold();
                        r.ConstantItem(120).AlignRight().Text($"{_invoice.TotalAmount:C}").Bold();
                    });
                });
            });
        }

        private void ComposeNotes(IContainer container)
        {
            if (string.IsNullOrWhiteSpace(_invoice.Notes))
                return;

            container.Column(col =>
            {
                col.Spacing(4);
                col.Item().PaddingTop(12).Text("Notes").Bold();
                col.Item().Text(_invoice.Notes!);
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(text =>
            {
                var details = new List<string>();
                if (!string.IsNullOrWhiteSpace(_companyAddress)) details.Add(_companyAddress);
                if (!string.IsNullOrWhiteSpace(_companyEmail)) details.Add(_companyEmail);
                if (!string.IsNullOrWhiteSpace(_companyPhone)) details.Add(_companyPhone);

                var footerLine = string.Join(" • ", details);

                if (!string.IsNullOrWhiteSpace(footerLine))
                {
                    text.Span(footerLine).FontSize(10).FontColor(Colors.Grey.Darken2);
                }
                else
                {
                    text.Span("Generated by QuickBill").FontSize(10).FontColor(Colors.Grey.Darken2);
                }
            });
        }
    }
}
