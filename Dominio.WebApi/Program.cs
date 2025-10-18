using AccesoDatos.EntityFramework;
using AccesoDatos.EntityFramework.Repositorios;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Microsoft.EntityFrameworkCore;

namespace Dominio.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Configuracion de Base de datos
            builder.Services.AddDbContext<DominioContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Dominio")
                )
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Repositorios
            builder.Services.AddScoped<IPagoRepositorio, RepositorioPagosEF>();

            //Casos de uso
            builder.Services.AddScoped<IObtenerPagoPorId, ObtenerPagoPorIdCU>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
