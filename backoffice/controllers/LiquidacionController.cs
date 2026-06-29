using Backoffice.Models;
using Backoffice.Models.Services;

namespace Backoffice.Controllers
{
    public class LiquidacionController
    {
        private LiquidacionServicio _servicio;
        
        public LiquidacionController(LiquidacionServicio servicio)
        {
            _servicio = servicio;
        }
        public bool AgregarLiquidacion(Liquidacion liquidacion) => _servicio.AgregarLiquidacion(liquidacion);
    }
}