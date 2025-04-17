namespace InvoiceApp.Models.Articles
{
    public class ArticleViewModel
    {
        public required int ArticleId { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
    }
}