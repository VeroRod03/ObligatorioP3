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

        public RepositorioPagosEF()
        {
            _context = new DominioContext();
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
            return _context.Pagos
                    .Include(pago => pago.Usuario);
        }

        public IEnumerable<Pago> FiltrarPagosPorFecha(Mes mes, int anio)
        {
            if(mes == 0 && anio == 0)
            {
                return FindAll();
            }
            else if (mes == 0)
            {
                return _context.Pagos
            .Include(pago => pago.Usuario)
            .Where(pago => pago.Fecha.Year == anio);
            }
            else if (anio == 0)
            {
                return _context.Pagos
                .Include(pago => pago.Usuario)
                .Where(pago => pago.Fecha.Month == (int)mes);
            }

            return _context.Pagos
                .Include(pago => pago.Usuario)
                .Where(pago => pago.Fecha.Month == (int)mes && pago.Fecha.Year == anio);
        }

        public Pago FindById(int id)
        {
            return _context.Pagos
                    .Where(pago => pago.Id == id)
                    .FirstOrDefault();
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
