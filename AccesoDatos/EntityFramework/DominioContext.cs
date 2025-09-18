using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework
{
    public class DominioContext : DbContext
    {
        public DbSet<TipoGasto> TipoGastos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Recurrente> Recurrentes { get; set; }
        public DbSet<Unico> Unicos { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"SERVER=(localdb)\MsSqlLocalDb;DATABASE=Dominio;Integrated Security = true;");
        }
    }
}
