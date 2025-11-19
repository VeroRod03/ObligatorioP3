using Dominio.Entidades;
using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework.Repositorios
{
    public class RepositorioPagosEF : IPagoRepositorio
    {
        private DominioContext _context;

        public RepositorioPagosEF(DominioContext context)
        {
            _context = context;
        }

        public void Add(Pago obj)
        {
            try
            {
                obj.Validar();         
                _context.Pagos.Add(obj);
                _context.SaveChanges();
            }
            catch (PagoException pe)
            {
                throw pe;
            }
            catch (Exception ex)
            {
                throw new PagoException("Hubo un error: ", ex);
            }

        }

        public IEnumerable<Pago> FindAll()
        {
            //hacemos el .count directamente en la vista; permitimos devolverla vacia

            return _context.Pagos
                        .Include(pago => pago.Usuario);
        }

        public IEnumerable<Pago> FiltrarPagosPorFecha(int mes, int anio)
        {
            return _context.Pagos
                .Include(pago => pago.Usuario)
                //EF no sabe ejecutar PagoIncluyeFecha en una consulta SQL
                //Con AsEnumberable() traemos los datos antes y luego filtramos en C# en lugar de SQL
                .AsEnumerable()
                .Where(pago => pago.PagoIncluyeFecha(mes, anio));
        }

        public Pago FindById(int id)
        {
            Pago aRetornar = _context.Pagos
                    .Where(pago => pago.Id == id)
                    .FirstOrDefault();
            if(aRetornar == null)
            {
                throw new PagoException("No hay ningun pago con ese id");
            }
            return aRetornar;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pago obj)
        {
            throw new NotImplementedException();
        }
    }
}
