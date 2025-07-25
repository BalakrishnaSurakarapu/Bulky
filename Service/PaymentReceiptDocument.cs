using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PaymentReceiptDocument : IDocument
{
    private readonly string paymentId;
    private readonly string orderId;
    private readonly decimal amount;
    private readonly string status;
    private readonly DateTime createdAt;

    public PaymentReceiptDocument(string paymentId, string orderId, decimal amount, string status, DateTime createdAt)
    {
        this.paymentId = paymentId;
        this.orderId = orderId;
        this.amount = amount;
        this.status = status;
        this.createdAt = createdAt;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4); // fixed size, single page
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(14));
            page.Content().Column(column =>
            {
                column.Spacing(10);

                //column.Item().Text("Payment Receipt")
                //             .FontSize(22).Bold().AlignCenter().PaddingBottom(20);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.ConstantColumn(150);
                        cols.RelativeColumn();
                    });

                    void Row(string label, string value)
                    {
                        table.Cell().Element(CellStyle).Text(label).SemiBold();
                        table.Cell().Element(CellStyle).Text(value);
                    }

                    Row("Payment ID", paymentId);
                    Row("Order ID", orderId);
                    Row("Amount", $"₹{amount}");
                    Row("Status", status);
                    Row("Date", createdAt.ToString("dd-MMM-yyyy hh:mm tt"));

                    static IContainer CellStyle(IContainer container) => container.PaddingVertical(5);
                });

                column.Item().PaddingTop(20).AlignRight()
                             .Text("Thank you for your payment!").Italic().FontSize(12);
            });
        });
    }
}
