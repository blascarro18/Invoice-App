# Invoice-App

Aplicación ASP.NET Core MVC, que incluye un pequeño módulo de facturación (Listar, crear y ver detalle de facturas) y una integración con el Web API REST de Novasoft para consultar cuentas de clientes. Ideal como base para pruebas técnicas.

## Características principales

- ✅ Módulo de **Facturas**: permite listar, crear y ver el detalle de facturas.
- ✅ Módulo de **Cuentas (Clientes)**: consumo de API externa Novasoft para listar y ver detalles de cuentas.
- ✅ Autenticación con token hacia Novasoft.
- ✅ Estilos con Bootstrap.
- ✅ Compatible con .NET 7 / .NET 8.

## Requisitos

- .NET SDK 7.0 o superior
- Visual Studio 2022 o VS Code
- Acceso a internet para consumir el API de Novasoft

## Configuración de la aplicación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/tu-usuario/InvoiceApp.git
   cd InvoiceApp
   ```

2. En el archivo **appsettings.json** Se encuentra la cadena de conexion a la BD (Configura tus credenciales):
   ```bash
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=InvoiceDB;User ID=sa;Password=root;TrustServerCertificate=True;"
   }
   ```
3. Aplica las migraciones (Usando Entity Framework Core, desde la consola):

   ```bash
   dotnet ef database update
   ```

## Correr la aplicación (Usando Visual Studio)

1. Abre el archivo `InvoiceApp.sln` desde Visual Studio 2022 (o superior).

2. Presiona `F5` o haz clic en **Iniciar depuración** para ejecutar la aplicación en modo desarrollo.

3. Visual Studio abrirá automáticamente tu navegador con la URL similar a: `http://localhost:xxxx`

## Correr la aplicación (Visual Studio Code o Consola)

1. Restaura los paquetes NuGet:

   ```bash
   dotnet restore
   ```

2. Ejecuta la app en modo watch (desarrollo):

   ```bash
   dotnet watch run
   ```

3. El navegador abrirála aplicación en:

   ```bash
   http://localhost:xxxx
   ```
