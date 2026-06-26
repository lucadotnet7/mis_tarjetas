namespace Backoffice.Models
{
    public sealed class Usuario
    {
        public string Documento { get; set; } = string.Empty;
        public ETipoDocumento TipoDocumento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = new DateTime(1926, 6, 26);
        public string CorreoElectronico { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string? Contrasenia { get; set; }
    }
}