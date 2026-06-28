using Backoffice.Models;
using Backoffice.Models.Services;

namespace Backoffice.Controllers
{
    public class UsuarioController
    {
        private UsuarioServicio _servicio;
        
        public UsuarioController(UsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        public List<Usuario> ObtenerUsuarios() => _servicio.ObtenerUsuarios();
        public bool AgregarUsuario(Usuario usuario) => _servicio.AgregarUsuario(usuario);

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOcurrió un error al intentar operar con la base de datos:");
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}