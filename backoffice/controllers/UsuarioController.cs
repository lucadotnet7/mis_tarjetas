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
        public Usuario? ObtenerUsuarioPorDocumento(string documento) => _servicio.ObtenerUsuarioPorDocumento(documento);
        public Usuario? ObtenerUsuarioPorCorreoElectronico(string correo) => _servicio.ObtenerUsuarioPorCorreoElectronico(correo);
        public bool EliminarUsuario(string documento) => _servicio.EliminarUsuario(documento);
    }
}