namespace InvoiceApp.Models.Novasoft;
public class AuthRequest
{
    public required string UserLogin { get; set; }
    public required string Password { get; set; }
    public required string ConnectionName { get; set; }
}

public class AuthResponse
{
    public required string Token { get; set; }
}
