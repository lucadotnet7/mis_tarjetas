namespace Backoffice.Models
{
    public sealed class Liquidacion
    {
        public int IdLiquidacion { get; set; }
        public int NumeroCuenta { get; set; }
        public string Periodo { get; set; } = "";
        public DateTime FechaVencimiento { get; set; } = new DateTime(2030, 1, 1);
        public decimal TotalPagar { get; set; }
        public decimal PagoMinimo { get; set; }
    }
}