using AccesoDatos.EntityFramework;
using AccesoDatos.EntityFramework.Repositorios;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.CasosDeUso.CasosEquipos;
using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Microsoft.EntityFrameworkCore;

namespace DominioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            //Configuracion de Base de datos
            builder.Services.AddDbContext<DominioContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Dominio")
                )
            );

            // Inicializamos Repositorios
            builder.Services.AddScoped<ITipoGastoRepositorio, RepositorioTipoGastosEF>();
            builder.Services.AddScoped<IUsuarioRepositorio, RepositorioUsuariosEF>();
            builder.Services.AddScoped<IAuditoriaRepositorio, RepositorioAuditoriasEF>();
            builder.Services.AddScoped<IPagoRepositorio, RepositorioPagosEF>();
            builder.Services.AddScoped<IEquipoRepositorio, RepositorioEquiposEF>();

            // Inicializamos Casos de Uso
            builder.Services.AddScoped<IObtenerTipoGastos, ObtenerTipoGastosCU>();
            builder.Services.AddScoped<IAltaTipoGasto, AltaTipoGastoCU>();
            builder.Services.AddScoped<IGetById, ObtenerTipoGastoPorIdCU>();
            builder.Services.AddScoped<IEditarTipoGasto, EditarTipoGastoCU>();
            builder.Services.AddScoped<IEliminarTipoGasto, EliminarTipoGastoCU>();

            builder.Services.AddScoped<IObtenerEquipos, ObtenerEquiposCU>();

            builder.Services.AddScoped<ILogin, LoginCU>();

            builder.Services.AddScoped<IAltaPago, AltaPagoCU>();
            builder.Services.AddScoped<IObtenerPagos, ObtenerPagosCU>();
            builder.Services.AddScoped<IObtenerPagosFiltrados, ObtenerPagosFiltradosCU>();

            builder.Services.AddScoped<IObtenerUsuarioPorId, ObtenerUsuarioPorIdCU>();
            builder.Services.AddScoped<IAltaUsuario, AltaUsuarioCU>();
            builder.Services.AddScoped<IObtenerUsuarios, ObtenerUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuariosFiltrados, ObtenerUsuariosFiltradosCU>();

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

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
