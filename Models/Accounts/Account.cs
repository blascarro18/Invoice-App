namespace InvoiceApp.Models.Accounts
{
    public class Account
    {
        public required string CodCli { get; set; }
        public required string TipIde { get; set; }
        public required string NitCli { get; set; }
        public required string DigVer { get; set; }
        public required string NomCli { get; set; }
        public required string Ap1Cli { get; set; }
        public required string Nom1Cli { get; set; }
        public required string Email { get; set; }
        public required int TipPer { get; set; }
        public required string CodCiu { get; set; }
        public required string CodDep { get; set; }
        public required string CodPai { get; set; }
        public required string Di1Cli { get; set; }
        public required string Te1Cli { get; set; }
        public required int TipCli { get; set; }
        public required DateTime FecIng { get; set; }
        public required string CodCliExtr { get; set; }
        public string? PagWeb { get; set; }
        public string? EstCli { get; set; }
        public string? Di2Cli { get; set; }
    }
}
