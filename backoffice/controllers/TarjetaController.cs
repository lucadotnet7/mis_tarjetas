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
        public Tarjeta? ObtenerTarjetaPorNumeroTarjeta(string numTarjeta) => _servicio.ObtenerTarjetaPorNumeroTarjeta(numTarjeta);
        public Tarjeta? ObtenerTarjetaPorNumeroCuenta(int numeroCuenta) => _servicio.ObtenerTarjetaPorNumeroCuenta(numeroCuenta);
        public List<UsuarioTarjeta> ObtenerDetalleTarjeta(int numeroCuenta) => _servicio.ObtenerDetalleTarjeta(numeroCuenta);
        public bool EliminarTarjeta(int numeroCuenta) => _servicio.EliminarTarjeta(numeroCuenta);
        public bool InsertarTarjeta(Tarjeta nuevaTarjeta) => _servicio.AgregarTarjeta(nuevaTarjeta);
        
    }
}