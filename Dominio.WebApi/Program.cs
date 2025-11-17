using AccesoDatos.EntityFramework;
using AccesoDatos.EntityFramework.Repositorios;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretTokenKey").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );


            builder.Services.AddSwaggerGen();
           // builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("Dominio.WebApi.xml")); ??

            //Repositorios
            builder.Services.AddScoped<IPagoRepositorio, RepositorioPagosEF>();

            //Casos de uso
            builder.Services.AddScoped<IObtenerPagoPorId, ObtenerPagoPorIdCU>();

            //para el token
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
