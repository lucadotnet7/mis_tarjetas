using Backoffice.Controllers;
using Backoffice.Models;
using Backoffice.Models.Services;
using Backoffice.Views.Utils;

namespace Backoffice.Views
{
    public class LiquidacionView
    {
        private readonly LiquidacionController _controller;
        private readonly TarjetaController _tarjetaController;

        public LiquidacionView()
        {
            _controller = new LiquidacionController(new LiquidacionServicio());
            _tarjetaController = new TarjetaController(new TarjetaServicio());
        }

        public void AgregarLiquidacion()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("    AGREGAR NUEVA LIQUIDACION - LIQUIDACIONES     ");
            Console.WriteLine("==================================================");
            Console.WriteLine("Número de cuenta: ");
            string? numCuenta = Console.ReadLine();
            Console.WriteLine("Periodo (aaaa/mm): ");
            string? periodo = Console.ReadLine();
            Console.WriteLine("Fecha de vencimiento (aaaa/mm/dd): ");
            string? fechaVencimiento = Console.ReadLine();
            Console.WriteLine("Total a pagar: ");
            string? totalPagar = Console.ReadLine();
            Console.WriteLine("Pago mínimo: ");
            string? pagoMinimo = Console.ReadLine();

            if(!StringUtils.StringValido(numCuenta) || !StringUtils.StringValido(periodo) 
                || !StringUtils.StringValido(fechaVencimiento) || !StringUtils.StringValido(fechaVencimiento)
                || !StringUtils.StringValido(totalPagar) || !StringUtils.StringValido(pagoMinimo))
            {
                Console.WriteLine("Los datos ingresados no son correctos. Intente nuevamente.");
                Console.ReadLine();
                return;
            }

            decimal.TryParse(totalPagar, out decimal totalPagarParseado);
            decimal.TryParse(pagoMinimo, out decimal pagoMinimoParseado);
            int.TryParse(numCuenta, out int numCuentaParseado);

            if(pagoMinimoParseado >= totalPagarParseado)
            {
                Console.WriteLine("El pago minimo no puede ser superior al monto total a pagar.");
                Console.ReadLine();
                return;
            }

            DateTime? periodoValido = DateUtils.ValidarFecha(periodo, "yyyy/MM");

            if(periodoValido is null)
            {
                Console.WriteLine("El periodo para la liquidación no es válido. Intente nuevamente.");
                Console.ReadLine();
                return;
            }

            DateTime? fechaVencimientoValida = DateUtils.ValidarFecha(fechaVencimiento, "yyyy/MM/dd");

            if(fechaVencimientoValida is null)
            {
                Console.WriteLine("La fecha de vencimiento ingresada no es valida. Intente nuevamente.");
                Console.ReadLine();
                return;
            }

            var existeTarjeta = _tarjetaController.ObtenerTarjetaPorNumeroCuenta(numCuentaParseado);

            if(existeTarjeta is null)
            {
                Console.WriteLine("No se puede agregar una liquidación a un numero de cuenta que no existe.");
                Console.ReadLine();
                return;
            }

            var liquidacionNueva = new Liquidacion
            {
                NumeroCuenta = numCuentaParseado,
                Periodo = periodoValido.Value.ToString("yyyy/MM")!,
                FechaVencimiento = (DateTime)fechaVencimientoValida!,
                TotalPagar = totalPagarParseado,
                PagoMinimo = pagoMinimoParseado,
            };

            bool resultado = _controller.AgregarLiquidacion(liquidacionNueva);

            if(resultado)
            {
                Console.WriteLine("Liquidación cargada exitosamente!.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error intentando crear la liquidación. Intente nuevamente.");
                Console.ReadLine();
                return;
            }
        }
    }
}