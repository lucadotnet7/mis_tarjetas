using Backoffice.Models;
using Backoffice.Models.Services;

namespace Backoffice.Controllers
{
    public class TarjetaController
    {
        private TarjetaServicio _servicio;
        
        public TarjetaController(TarjetaServicio servicio)
        {
            _servicio = servicio;
        }

        public List<Tarjeta> ObtenerTarjetas() => _servicio.ObtenerTarjetas();

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOcurrió un error al intentar operar con la base de datos:");
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}