using Backoffice.Controllers;
using Backoffice.Models.Services;

namespace Backoffice.Views
{
    public class TarjetaView
    {
        private readonly TarjetaController _controller;
        private readonly TarjetaServicio _servicio = new TarjetaServicio();

        public TarjetaView()
        {
            _controller = new TarjetaController(_servicio);
        }

        public void Init()
        {
            bool volver = false;

            while(!volver)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("      BACKOFFICE BANCARIO MI BANCO - TARJETAS     ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Ver tarjetas");
                Console.WriteLine("4. Volver al menú principal");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");
                
                string? opcion = Console.ReadLine();

                if(!string.IsNullOrEmpty(opcion))
                {
                    switch(opcion)
                    {
                        case "1":
                            MostrarTarjetas();
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

        private void MostrarTarjetas()
        {
            var tarjetas = _controller.ObtenerTarjetas();
            Console.WriteLine(string.Format("{0,-20} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5,-25}", 
                    "Numero de Cuenta", "Numero de Tarjeta", "Banco Emisor", "Estado", "Saldo", "Dni del Titular"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var tarjeta in tarjetas)
            {
                Console.WriteLine(string.Format("{0,-20} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5,-25}", 
                    tarjeta.NumeroCuenta, tarjeta.NumeroTarjeta, tarjeta.Banco, tarjeta.Estado, tarjeta.Saldo, tarjeta.DniTitular));
            }
            Console.WriteLine("=============================================================================================================================================\n");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}