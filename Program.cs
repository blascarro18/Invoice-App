using InvoiceApp.Database;
using InvoiceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<NovasoftApiService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Invoices");
    return Task.CompletedTask;
});


app.MapControllerRoute(
    name: "Invoices",
    pattern: "Invoices/{action=Index}/{id?}",
    defaults: new { controller = "Invoice" }
);

app.MapControllerRoute(
    name: "Accounts",
    pattern: "Accounts/{action=Index}/{id?}",
    defaults: new { controller = "Account" }
);

app.MapControllerRoute(
    name: "Invoices",
    pattern: "{controller=Invoice}/{action=Index}/{id?}"
);

app.Run();
