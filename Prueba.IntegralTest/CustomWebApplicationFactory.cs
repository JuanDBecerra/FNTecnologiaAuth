using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Prueba.Domain.Interfaces;
using Prueba.Infraestructure.Contexts.Context;
using System.Linq;

namespace Prueba.IntegralTest
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IAuthService> AuthServiceMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Eliminar TODOS los descriptores relacionados con DbContext
                var dbContextDescriptors = services.Where(d =>
                    d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions) ||
                    d.ServiceType == typeof(AppDbContext) ||
                    d.ImplementationType == typeof(AppDbContext))
                    .ToList();

                foreach (var descriptor in dbContextDescriptors)
                {
                    services.Remove(descriptor);
                }

                // Registrar DbContext con InMemory - usando un nombre único por instancia
                var dbName = $"TestDb_{Guid.NewGuid()}";
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(dbName);
                    options.EnableSensitiveDataLogging(); // Solo para pruebas
                });

                // Eliminar registro IAuthService si existe y agregar mock
                var authServiceDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IAuthService));
                if (authServiceDescriptor != null)
                {
                    services.Remove(authServiceDescriptor);
                }

                services.AddSingleton(AuthServiceMock.Object);

                // Opcional: Deshabilitar logs innecesarios en pruebas
                services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Warning));
            });

            // Configurar entorno de pruebas
            builder.UseEnvironment("Testing");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Limpiar recursos si es necesario
                try
                {
                    using var scope = Services.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureDeleted();
                }
                catch
                {
                    // Ignorar errores al limpiar
                }
            }

            base.Dispose(disposing);
        }
    }
}