using System.Text;
using Backoffice.Controllers;
using Backoffice.Models;
using Backoffice.Models.Constants;
using Backoffice.Models.Services;
using Backoffice.Views.Utils;

namespace Backoffice.Views
{
    public class TarjetaView
    {
        private readonly TarjetaController _controller;
        private readonly UsuarioController _usuarioController;

        public TarjetaView()
        {
            _controller = new TarjetaController(new TarjetaServicio());
            _usuarioController = new UsuarioController(new UsuarioServicio());
        }

        public void MostrarTarjetas()
        {
            var tarjetas = _controller.ObtenerTarjetas();
            Console.Clear();
            Console.WriteLine("--- LISTADO GENERAL DE TARJETAS ---");
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

        public void AgregarTarjeta(string dniTitular)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("         AGREGAR NUEVA TARJETA - TARJETAS         ");
            Console.WriteLine("==================================================");
            string? banco = SeleccionBanco();
            if(string.IsNullOrEmpty(banco))
                return;
            Console.WriteLine("Saldo inicial: ");
            string? saldoInicial = Console.ReadLine();

            if(!StringUtils.StringValido(saldoInicial))
            {
                Console.WriteLine("Los datos ingresados no son correctos. Intente nuevamente.");
                Console.ReadLine();
                return;
            }

            string numeroTarjeta = GenerarNumeroTarjeta();

            Tarjeta? existeTarjeta = _controller.ObtenerTarjetaPorNumeroTarjeta(numeroTarjeta);

            if(existeTarjeta is not null)
            {
                Console.WriteLine($"Ya existe una tarjeta con el número asociado: {numeroTarjeta}.");
                Console.ReadLine();
                return;
            }

            var nuevaTarjeta = new Tarjeta
            {
                NumeroTarjeta = GenerarNumeroTarjeta(),
                Banco = banco,
                Estado = EEstadoTarjeta.ACTIVA,
                Saldo = decimal.Parse(saldoInicial!),
                DniTitular = dniTitular
            };

            bool resultado = _controller.InsertarTarjeta(nuevaTarjeta);

            if(resultado)
            {
                Console.WriteLine("Tarjeta creada exitosamente!.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error intentando crear la tarjeta. Intente nuevamente.");
                Console.ReadLine();
                return;
            }
        }

        public void VerDetalleTarjeta()
        {
            Console.Clear();
            Console.WriteLine("--- DETALLE DE TARJETA Y CLIENTE ---");
            Console.Write("Ingrese el Número de Cuenta a consultar: ");
            int numCuenta = Convert.ToInt32(Console.ReadLine());

            List<UsuarioTarjeta> usuariosTarjetas = _controller.ObtenerDetalleTarjeta(numCuenta);

            Console.WriteLine(string.Format("{0,-20} | {1,-20} | {2,-25} | {3,-20} | {4,-20} | {5,-20} | {6, -25} | {7, -20}", 
                    "Nombre", "Apellido", "Correo electrónico", "Documento", "Numero de cuenta", "Estado de tarjeta", "Numero de tarjeta", "Saldo"));
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var usuarioTarjeta in usuariosTarjetas)
            {
                Console.WriteLine(string.Format("{0,-20} | {1,-20} | {2,-25} | {3,-20} | {4,-20} | {5,-20} | {6, -25} | {7, -20}", 
                    usuarioTarjeta.Nombre, usuarioTarjeta.Apellido, usuarioTarjeta.CorreoElectronico, 
                    usuarioTarjeta.Documento, usuarioTarjeta.NumeroCuenta, usuarioTarjeta.EstadoTarjeta, 
                     usuarioTarjeta.NumeroTarjeta,  usuarioTarjeta.Saldo));
            }
            Console.WriteLine("=======================================================================================================================================================================================\n");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void EliminarTarjeta()
        {
            Console.Clear();
            Console.WriteLine("--- ELIMINAR TARJETA DEL SISTEMA ---");
            Console.Write("Ingrese el Número de Cuenta de la tarjeta a dar de baja: ");
            int numCuenta = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️ ADVERTENCIA: Se eliminará la tarjeta, sus liquidaciones y los datos de acceso web vinculados.");
            Console.ResetColor();
            Console.Write("¿Está seguro de continuar? (S/N): ");
            
            if (Console.ReadLine()?.ToUpper() == "S")
            {
                Tarjeta? tarjetaAEliminar = _controller.ObtenerTarjetaPorNumeroCuenta(numCuenta);

                if(tarjetaAEliminar is not null)
                {
                    bool exito = _controller.EliminarTarjeta(numCuenta);

                    if (exito)
                    {
                        _usuarioController.EliminarUsuario(tarjetaAEliminar.DniTitular);
                        Console.WriteLine("\nTarjeta eliminada correctamente del sistema.");
                    }
                    else
                        Console.WriteLine("\nError al intentar eliminar la tarjeta. Verifique el número de cuenta.");
                }
                else
                {
                    Console.WriteLine("El número de cuenta que intenta eliminar no existe.");
                }
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        private string SeleccionBanco()
        {
            Console.WriteLine("Banco Emisor: ");
            for (int i = 0; i < CBancos.Bancos.Count; i++)
            {
                Console.WriteLine($"{i+1} - {CBancos.Bancos[i]}");
            }
            string? opcion = Console.ReadLine();
            string? banco = null;
            
            if(int.TryParse(opcion, out int opcionParseada) && opcionParseada >= 1 && opcionParseada <= CBancos.Bancos.Count)
            {
                banco = CBancos.Bancos[opcionParseada - 1];
                return banco;
            }
            else
            {
                Console.WriteLine("Banco seleccionado inválido.");
                Console.ReadLine();
                return string.Empty;
            }
        }

        private string GenerarNumeroTarjeta()
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder(16);

            for (int i = 0; i < 16; i++)
            {
                sb.Append(rand.Next(0, 10)); 
            }

            return sb.ToString();
        }
    }
}