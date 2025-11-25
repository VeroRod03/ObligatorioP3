namespace Dominio.LogicaAplicacion.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; } //?
        public int TipoGastoId { get; set; }
    }
}