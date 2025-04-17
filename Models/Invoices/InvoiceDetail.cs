namespace InvoiceApp.Models.Invoices
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public required int InvoiceId { get; set; } // Clave foránea hacia Invoice
        public required int ArticleId { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }

        // Propiedad de navegación hacia Invoice
        public required Invoice Invoice { get; set; }
    }
}
