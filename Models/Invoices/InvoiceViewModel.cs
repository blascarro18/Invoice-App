namespace InvoiceApp.Models.Invoices;
using InvoiceApp.Models.Articles;
public class InvoiceViewModel
{
    public required DateTime InvoiceDate { get; set; }
    public required string CliDocument { get; set; }
    public required string CliNames { get; set; }
    public required string CliLastNames { get; set; }
    public required string CliAddress { get; set; }
    public required string CliPhone { get; set; }
    public required List<ArticleViewModel> Articles { get; set; } = [];
}
