namespace Dominio.Exceptions
{

    public class OperacionConflictivaException : Exception
    {
        public OperacionConflictivaException() { }
        public OperacionConflictivaException(string mensaje) : base(mensaje) { }
        public OperacionConflictivaException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}