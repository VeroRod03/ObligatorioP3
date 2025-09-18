using AccesoDatos.EntityFramework.Repositorios;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;

namespace DominioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Inicializamos Repositorios
            builder.Services.AddScoped<ITipoGastoRepositorio, RepositorioTipoGastosEF>();

            // Inicializamos Casos de Uso
            builder.Services.AddScoped<IObtenerTipoGastos, ObtenerTipoGastosCU>();
            builder.Services.AddScoped<IAltaTipoGasto, AltaTipoGastoCU>();
            builder.Services.AddScoped<IGetById, ObtenerTipoGastoPorIdCU>();
            builder.Services.AddScoped<IEditarTipoGasto, EditarTipoGastoCU>();
            builder.Services.AddScoped<IEliminarTipoGasto, EliminarTipoGastoCU>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
