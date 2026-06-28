using Backoffice.Controllers;
using Backoffice.Models;
using Backoffice.Models.Services;

namespace Backoffice.Views
{
    public class UsuarioView
    {
        private readonly UsuarioController _controller;
        private readonly UsuarioServicio _usuarioServicio = new UsuarioServicio();

        public UsuarioView()
        {
            _controller = new UsuarioController(_usuarioServicio);
        }

        public void Init()
        {
            bool volver = false;

            while(!volver)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("      BACKOFFICE BANCARIO MI BANCO - USUARIOS     ");
                Console.WriteLine("1. Ver usuarios");
                Console.WriteLine("2. Cargar usuario");
                Console.WriteLine("2. Editar usuario");
                Console.WriteLine("2. Eliminar usuario");
                Console.WriteLine("4. Volver al menú principal");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");
                
                string? opcion = Console.ReadLine();

                if(!string.IsNullOrEmpty(opcion))
                {
                    switch(opcion)
                    {
                        case "1":
                            MostrarUsuarios();
                            break;
                        case "2":
                            AgregarUsuario();
                            break;
                        case "4":
                            MainMenu.Start();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida.");
                    Environment.Exit(0);
                }
            }
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

        private void AgregarUsuario()
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

            if(!StringValido(nombre) || !StringValido(apellido) || !StringValido(documento) || !StringValido(correoElectronico))
            {
                Console.WriteLine("Los datos ingresados no son validos. Intente nuevamente.");
                Console.ReadLine();
                Init();
            }

            if(!StringValido(tipoDocumentoOpcion) || (tipoDocumentoOpcion != "1" && tipoDocumentoOpcion != "2"))
            {
                Console.WriteLine("El tipo de documento ingresado no es valido. Intente nuevamente.");
                Console.ReadLine();
                Init();
            }

            DateTime? fechaValida = ValidarFecha(fechaNacimiento);

            if(fechaValida is null)
            {
                Console.WriteLine("La fecha de nacimiento ingresada no es valida. Intente nuevamente.");
                Console.ReadLine();
                Init();
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

            bool resultado = _controller.AgregarUsuario(nuevoUsuario);

            if(resultado)
            {
                Console.WriteLine("Usuario agregado exitosamente!.");
                Console.ReadLine();
                Init();
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error intentando agregar el usuario. Intente nuevamente.");
                Console.ReadLine();
                Init();
            }
        }

        public bool StringValido(string? valor)
        {
            if(string.IsNullOrEmpty(valor))
                return false;
            else
                return true;
        }

        public DateTime? ValidarFecha(string? fecha)
        {
            if(StringValido(fecha))
            {
                string formatoFecha = "yyyy/MM/dd";
                bool esFechaValida = DateTime.TryParseExact(
                    fecha,
                    formatoFecha,
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out DateTime fechaValida
                );

                if(esFechaValida)
                    return fechaValida;
                else
                    return null;
            }

            return null;
        }
    }
}