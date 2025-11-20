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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            //para el token
            builder.Services.AddAuthentication(
                aut =>
                {
                    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                opciones =>
                {
                    opciones.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretTokenKey").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );


            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("Dominio.WebApi.xml")); 

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
            builder.Services.AddScoped<IObtenerPagoPorId, ObtenerPagoPorIdCU>();

            builder.Services.AddScoped<IAltaUsuario, AltaUsuarioCU>();
            builder.Services.AddScoped<IObtenerUsuarios, ObtenerUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuariosFiltrados, ObtenerUsuariosFiltradosCU>();

            //para el la configuracion del token
            builder.Services.AddAuthorization(
                options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                }
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //autenticacion y autorizacion
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
