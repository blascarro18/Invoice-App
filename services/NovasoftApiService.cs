using System.Net.Http.Headers;
using InvoiceApp.Models.Accounts;
using InvoiceApp.Models.Novasoft;

namespace InvoiceApp.Services
{
    public class NovasoftApiService
    {
        private readonly HttpClient _httpClient;

        public NovasoftApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<string?> GetToken()
        {
            var login = new AuthRequest
            {
                UserLogin = "pruebaTecnica",
                Password = "P@ssw0rd",
                ConnectionName = "DataPower"
            };

            var response = await _httpClient.PostAsJsonAsync("https://test.novasoft.com.co:8091/WebAPI/api/Cuenta/Login", login);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return token?.Token;
            }

            throw new Exception("No se pudo obtener el token.");
        }

        public async Task<bool> CreateAccount(Account account)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("https://test.novasoft.com.co:8091/WebAPI/api/CXC/Senior/Accounts", account);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Account>> ListAccounts()
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://test.novasoft.com.co:8091/WebAPI/api/CXC/Senior/Accounts");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al listar las cuentas.");

            var accounts = await response.Content.ReadFromJsonAsync<List<Account>>();
            return accounts ?? new List<Account>();
        }

        public async Task<Account?> GetAccountById(string id)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"https://test.novasoft.com.co:8091/WebAPI/api/CXC/Senior/Accounts/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            // Deserializar el contenido como un array
            var accountsArray = await response.Content.ReadFromJsonAsync<List<Account>>();

            // Si el array tiene elementos, devolvemos el primer elemento
            return accountsArray?.FirstOrDefault();
        }

    }
}
