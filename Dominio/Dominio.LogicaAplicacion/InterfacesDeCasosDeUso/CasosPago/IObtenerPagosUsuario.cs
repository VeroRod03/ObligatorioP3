using Dominio.LogicaAplicacion.DTOs;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago
{
    public interface IObtenerPagosUsuario
    {
        public IEnumerable<PagoDTO> ObtenerPagosUsuario(int id);
    }
}
