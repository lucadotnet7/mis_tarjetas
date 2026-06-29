using System.Linq.Expressions;
using Backoffice.Controllers;
using Backoffice.Models;
using Backoffice.Models.Services;
using Backoffice.Views.Utils;

namespace Backoffice.Views
{
    public class UsuarioView
    {
        private readonly UsuarioController _controller;

        public UsuarioView()
        {
            _controller = new UsuarioController(new UsuarioServicio());
        }

        private void MostrarUsuarios()
        {
            var usuarios = _controller.ObtenerUsuarios();
            Console.WriteLine(string.Format("{0,-10} | {1,-10} | {2,-20} | {3,-20} | {4,-20} | {5,-25} | {6, -10}", 
                    "Nombre", "Apellido", "Nro Documento", "Tipo de Documento", "Fecha de Nacimiento", "Correo electronico", "Usuario"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine(string.Format("{0,-10} | {1,-10} | {2,-20} | {3,-20} | {4,-20} | {5,-25} | {6, -10}", 
                    usuario.Nombre, usuario.Apellido, usuario.Documento, usuario.TipoDocumento, usuario.FechaNacimiento, usuario.CorreoElectronico, usuario.NombreUsuario));
            }
            Console.WriteLine("=============================================================================================================================================\n");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public string AgregarUsuario()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("         AGREGAR NUEVO USUARIO - USUARIOS         ");
            Console.WriteLine("==================================================");
            Console.WriteLine("Nombre: ");
            string? nombre = Console.ReadLine();
            Console.WriteLine("Apellido: ");
            string? apellido = Console.ReadLine();
            Console.WriteLine("Fecha de nacimiento (aaaa/mm/dd): ");
            string? fechaNacimiento = Console.ReadLine();
            Console.WriteLine("Tipo Documento");
            Console.WriteLine("1. DNI");
            Console.WriteLine("2. PASAPORTE");
            string? tipoDocumentoOpcion = Console.ReadLine();
            Console.WriteLine("Documento: ");
            string? documento = Console.ReadLine();
            Console.WriteLine("Correo electronico: ");
            string? correoElectronico = Console.ReadLine();

            if(!StringUtils.StringValido(nombre) || !StringUtils.StringValido(apellido) || !StringUtils.StringValido(documento) || !StringUtils.StringValido(correoElectronico))
            {
                Console.WriteLine("Los datos ingresados no son validos. Intente nuevamente.");
                Console.ReadLine();
                return string.Empty;
            }

            if(!StringUtils.StringValido(tipoDocumentoOpcion) || (tipoDocumentoOpcion != "1" && tipoDocumentoOpcion != "2"))
            {
                Console.WriteLine("El tipo de documento ingresado no es valido. Intente nuevamente.");
                Console.ReadLine();
                return string.Empty;
            }

            DateTime? fechaValida = DateUtils.ValidarFecha(fechaNacimiento, "yyyy/MM/dd");

            if(fechaValida is null || fechaValida >= DateTime.Today)
            {
                Console.WriteLine("La fecha de nacimiento ingresada no es valida. Intente nuevamente.");
                Console.ReadLine();
                return string.Empty;
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre!,
                Apellido = apellido!,
                TipoDocumento = tipoDocumentoOpcion == "1" ? ETipoDocumento.DNI : ETipoDocumento.PASAPORTE,
                Documento = documento!,
                FechaNacimiento = (DateTime)fechaValida!,
                CorreoElectronico = correoElectronico!,
            };

            Usuario? usuarioMismoDoc = _controller.ObtenerUsuarioPorDocumento(nuevoUsuario.Documento);
            Usuario? usuarioMismoCorreo = _controller.ObtenerUsuarioPorCorreoElectronico(nuevoUsuario.CorreoElectronico);

            if(usuarioMismoDoc is not null)
            {
                Console.WriteLine($"El usuario con documento: {nuevoUsuario.Documento} ya existe.");
                Console.ReadLine();
                return string.Empty;
            }

            if(usuarioMismoCorreo is not null)
            {
                Console.WriteLine($"El usuario con correo electronico: {nuevoUsuario.CorreoElectronico} ya existe.");
                Console.ReadLine();
                return string.Empty;
            }

            bool resultado = _controller.AgregarUsuario(nuevoUsuario);

            if(resultado)
            {
                Console.WriteLine("Usuario agregado exitosamente!.");
                Console.ReadLine();
                return nuevoUsuario.Documento;
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error intentando agregar el usuario. Intente nuevamente.");
                Console.ReadLine();
                return string.Empty;
            }
        }
    }
}