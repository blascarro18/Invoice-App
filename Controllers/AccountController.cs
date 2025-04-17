using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvoiceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly NovasoftApiService _apiService;

        public AccountController(ILogger<AccountController> logger, NovasoftApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            // Obtiene la lista de cuentas y la pasa directamente a la vista
            var accounts = await _apiService.ListAccounts();
            return View(accounts);
        }

        public async Task<IActionResult> Details(string id)
        {
            // Obtiene la información de la cuenta por ID y la pasa a la vista
            var account = await _apiService.GetAccountById(id);
            _logger.LogInformation("JSON recibido: " + account); // Ver el JSON recibido

            if (account == null)
            {
                return NotFound("Cuenta no encontrada.");
            }

            return View(account);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
