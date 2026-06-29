namespace Backoffice.Models
{
    public class UsuarioTarjeta
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Documento { get; set;} = string.Empty;
        public int NumeroCuenta { get; set; }
        public EEstadoTarjeta EstadoTarjeta { get; set; }
        public string NumeroTarjeta { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }
}