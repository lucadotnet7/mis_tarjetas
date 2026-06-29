namespace Backoffice.Views
{
    public class MainMenu
    {
        private static readonly UsuarioView _usuarioView = new UsuarioView();
        private static readonly TarjetaView _tarjetaView = new TarjetaView();
        private static readonly LiquidacionView _liquidacionView = new LiquidacionView();

        public static void Start()
        {
            string? opcion = "";
            while(opcion != "6")
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("          BACKOFFICE BANCARIO MI BANCO            ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Agregar usuario");
                Console.WriteLine("2. Agregar liquidación");
                Console.WriteLine("3. Listar tarjetas.");
                Console.WriteLine("4. Detalle de tarjetas.");
                Console.WriteLine("5. Eliminar tarjeta.");
                Console.WriteLine("6. Salir");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");
                    
                opcion = Console.ReadLine();

                if(!string.IsNullOrEmpty(opcion))
                {
                    switch(opcion)
                    {
                        case "1":
                            string dniTitular = _usuarioView.AgregarUsuario();
                            if(!string.IsNullOrEmpty(dniTitular))
                                _tarjetaView.AgregarTarjeta(dniTitular);
                            break;
                        case "2":
                                _liquidacionView.AgregarLiquidacion();
                            break;
                        case "3":
                            _tarjetaView.MostrarTarjetas();
                            break;
                        case "4":
                            _tarjetaView.VerDetalleTarjeta();
                            break;
                        case "5":
                            _tarjetaView.EliminarTarjeta();
                            break;
                        case "6":
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
}