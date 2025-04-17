namespace InvoiceApp.Models.Invoices
{
    public class Invoice
    {
        public int Id { get; set; }
        public required DateTime InvoiceDate { get; set; }
        public required string CliDocument { get; set; }
        public required string CliNames { get; set; }
        public required string CliLastNames { get; set; }
        public required string CliAddress { get; set; }
        public required string CliPhone { get; set; }

        // Propiedad de navegación para los detalles de la factura
        public required ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
