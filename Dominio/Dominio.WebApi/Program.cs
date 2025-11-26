using AccesoDatos.EntityFramework;
using AccesoDatos.EntityFramework.Repositorios;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.CasosDeUso.CasosAuditoria;
using Dominio.LogicaAplicacion.CasosDeUso.CasosEquipos;
using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosAuditoria;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
                    builder.Configuration.GetConnectionString("MyWallet")
                )
            );

            var key = builder.Configuration["SecretTokenKey"];
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
            System.Diagnostics.Debug.WriteLine($"CLAVE USADA: '{key}'");
            System.Diagnostics.Debug.WriteLine($"Caracteres: {key.Length}, Bytes: {keyBytes.Length}");




            //para el token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones =>
                {
                    opciones.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretTokenKey").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                }
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(opciones =>
            {
                opciones.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Autorizaci�n est�ndar mediante esquema Bearer",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                opciones.OperationFilter<SecurityRequirementsOperationFilter>();

                opciones.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Documentaci�n de Dominio.Api",
                    Description = "Aqui se encuentran todos los endpoint activos para utilizar los servicios del proyecto My Wallet",
                    Contact = new OpenApiContact
                    {
                        Email = "diegomdiaz15@gmail.com , veronicarod2003@gmail.com"

                    },
                    Version = "v1"
                });

                opciones.IncludeXmlComments("Dominio.WebApi");
            });

            //builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("Dominio.WebApi")); 

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
            builder.Services.AddScoped<IObtenerEquiposFiltrados, ObtenerEquiposFiltradosCU>();
            
            builder.Services.AddScoped<ILogin, LoginCU>();

            builder.Services.AddScoped<IAltaPago, AltaPagoCU>();
            builder.Services.AddScoped<IObtenerPagos, ObtenerPagosCU>();
            builder.Services.AddScoped<IObtenerPagosFiltrados, ObtenerPagosFiltradosCU>();
            builder.Services.AddScoped<IObtenerPagoPorId, ObtenerPagoPorIdCU>();
            builder.Services.AddScoped<IObtenerPagosUsuario, ObtenerPagosUsuarioCU>();

            
            builder.Services.AddScoped<IAltaUsuario, AltaUsuarioCU>();
            builder.Services.AddScoped<IObtenerUsuarios, ObtenerUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuariosFiltrados, ObtenerUsuariosFiltradosCU>();
            builder.Services.AddScoped<IGenerarContra, GenerarContraCU>();

            builder.Services.AddScoped<IObtenerAuditoriasTipoGasto, ObtenerAuditoriasTipoGastoCU>();

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
