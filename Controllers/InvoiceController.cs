using InvoiceApp.Database;
using InvoiceApp.Models;
using InvoiceApp.Models.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InvoiceApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;

        private readonly ApplicationDbContext _contextDB;

        public InvoiceController(ILogger<InvoiceController> logger, ApplicationDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contextDB = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IActionResult Index()
        {
            // Obtener todas las facturas de la base de datos
            var invoices = _contextDB.Invoices
                .Include(i => i.InvoiceDetails)
                .ToList();

            // Pasar las facturas a la vista
            return View(invoices);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("La información enviada no es válida.");
            }

            try
            {
                // Crear la nueva factura
                var newInvoice = new Invoice
                {
                    InvoiceDate = model.InvoiceDate,
                    CliDocument = model.CliDocument,
                    CliNames = model.CliNames,
                    CliLastNames = model.CliLastNames,
                    CliAddress = model.CliAddress,
                    CliPhone = model.CliPhone,
                    InvoiceDetails = new List<InvoiceDetail>() // Inicializamos la colección
                };

                // Agregar la nueva factura a la base de datos
                _contextDB.Invoices.Add(newInvoice);
                await _contextDB.SaveChangesAsync(); // Guardar la factura

                // Ahora, guardar los artículos relacionados
                foreach (var article in model.Articles)
                {
                    var newArticle = new InvoiceDetail // Aquí debemos usar InvoiceDetail en lugar de Article
                    {
                        ArticleId = article.ArticleId,
                        Quantity = article.Quantity,
                        Price = article.Price,
                        InvoiceId = newInvoice.Id, // Enlazar el artículo con la factura
                        Invoice = newInvoice // Establecer la relación con la factura
                    };

                    _contextDB.InvoiceDetails.Add(newArticle); // Asegúrate de agregar a InvoiceDetails
                }

                await _contextDB.SaveChangesAsync(); // Guardar los artículos relacionados

                // Devolver el ID de la factura creada
                return Ok(new { invoiceId = newInvoice.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hubo un error al crear la factura: " + ex.Message);
            }
        }

        public IActionResult Details(int id)
        {
            // Buscar la factura por ID, incluyendo los detalles
            var invoice = _contextDB.Invoices
                .Include(i => i.InvoiceDetails)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
