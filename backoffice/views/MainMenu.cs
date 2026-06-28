using Backoffice.Controllers;

namespace Backoffice.Views
{
    public class MainMenu
    {
        private static readonly UsuarioView _usuarioView = new UsuarioView();
        private static readonly TarjetaView _tarjetaView = new TarjetaView();

        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("          BACKOFFICE BANCARIO MI BANCO            ");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Gestionar usuarios");
            Console.WriteLine("2. Gestionar tarjetas");
            Console.WriteLine("3. Gestionar liquidaciones");
            Console.WriteLine("4. Salir");
            Console.WriteLine("==================================================");
            Console.Write("Seleccione una opción: ");
                
            string? opcion = Console.ReadLine();

            if(!string.IsNullOrEmpty(opcion))
            {
                switch(opcion)
                {
                    case "1":
                        _usuarioView.Init();
                        break;
                    case "2":
                        _tarjetaView.Init();
                        break;
                    case "3":
                        break;
                    case "4":
                        Environment.Exit(0);
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
}