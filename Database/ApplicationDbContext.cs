using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models.Invoices;

namespace InvoiceApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para cada uno de los modelos
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cambiamos el nombre de las tablas en la base de datos y configuramos las propiedades de las entidades
            // Configuración de la tabla Invoice
            modelBuilder.Entity<Invoice>().ToTable("Invoice");

            // Configuración de la propiedad Id (Número de factura)
            modelBuilder.Entity<Invoice>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd() // Para que sea autoincremental
                .HasColumnType("int")
                .UseIdentityColumn() // Identity
                .IsRequired(true);

            // Configuración de la propiedad Fecha de factura
            modelBuilder.Entity<Invoice>()
                .Property(i => i.InvoiceDate)
                .HasDefaultValueSql("GETDATE()") // Se establece la fecha del día como valor por defecto
                .ValueGeneratedOnAdd() // Solo lectura, se genera automáticamente al insertar
                .HasColumnType("datetime")
                .IsRequired(true);

            // Configuración de las propiedades de cliente
            modelBuilder.Entity<Invoice>()
                .Property(i => i.CliDocument)
                .HasMaxLength(15) // Longitud máxima de 15 caracteres para el documento
                .HasColumnType("varchar(15)")
                .IsRequired(true);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CliNames)
                .HasMaxLength(30) // Longitud máxima de 30 caracteres para el nombre del cliente
                .HasColumnType("varchar(30)")
                .IsRequired(true);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CliLastNames)
                .HasMaxLength(30) // Longitud máxima de 30 caracteres para el apellido del cliente
                .HasColumnType("varchar(30)")
                .IsRequired(true);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CliAddress)
                .HasMaxLength(30) // Longitud máxima de 30 caracteres para la direccion del cliente
                .HasColumnType("varchar(30)")
                .IsRequired(true);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CliPhone)
                .HasMaxLength(10) // Longitud máxima de 10 caracteres para el telefono del cliente
                .HasColumnType("varchar(10)")
                .IsRequired(true);


            // Configuración de la tabla InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>().ToTable("InvoiceDetails");

            modelBuilder.Entity<InvoiceDetail>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd() // Para que sea autoincremental
                .HasColumnType("int")
                .UseIdentityColumn() // Identity
                .IsRequired(true);

            // Relación de uno a muchos entre Invoice y InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(i => i.Invoice) // Una factura (Invoice) tiene muchos detalles (InvoiceDetail)
                .WithMany(i => i.InvoiceDetails) // Un detalle de factura (InvoiceDetail) pertenece a una sola factura
                .HasForeignKey(i => i.InvoiceId) // La clave foránea es InvoiceId en InvoiceDetail
                .IsRequired(); // La relación es obligatoria (no puede ser nula)

            modelBuilder.Entity<InvoiceDetail>()
                .Property(i => i.ArticleId)
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(i => i.Quantity)
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(i => i.Price)
                .HasConversion(
                    v => (decimal)v,    // Convertir el valor de 'decimal' a 'money' al guardar en la base de datos
                    v => (decimal)v)    // Convertir el valor de 'money' a 'decimal' al leer de la base de datos
                .HasColumnType("money");  // Especificar el tipo de columna como 'money' en la base de datos


            base.OnModelCreating(modelBuilder);
        }
    }
}
