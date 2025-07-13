using Microsoft.EntityFrameworkCore;
using Prueba.Application.Services;
using Prueba.Domain.Interfaces;
using Prueba.Infraestructure.Contexts;
using Prueba.Infraestructure.Contexts.Context;

namespace Prueba.Adapters.API
{
    public class ConfigureServices
    {
        private readonly IConfiguration _configuration;

        public ConfigureServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                    builder.WithOrigins("http://localhost:4200") // La URL de tu frontend Angular
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            });
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
