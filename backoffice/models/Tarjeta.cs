using Backoffice.Models.Constants;

namespace Backoffice.Models
{
    public sealed class Tarjeta
    {
        private string  _banco = "";

        public int NumeroCuenta { get; set; }
        public string NumeroTarjeta { get; set; } = string.Empty;
        public string Banco
        {
            get => _banco;
            set
            {
                if(!CBancos.Bancos.Contains(value))
                {
                    throw new Exception($"El banco {value} no es una opción válida.");
                }
                _banco = value;
            }
        }
        public EEstadoTarjeta Estado { get; set; }
        public decimal Saldo { get; set; }
        public string DniTitular { get; set; } = string.Empty;
    }
}