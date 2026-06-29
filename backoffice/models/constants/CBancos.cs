namespace Backoffice.Models.Constants
{
    public static class CBancos
    {
        public const string BancoNacion = "Banco Nación";
        public const string BancoProvincia = "Banco Provincia";
        public const string BancoGalicia = "Banco Galicia";
        public const string BancoSantander = "Banco Santander";
        public const string BancoFrances = "Banco BBVA";
        public const string BancoMacro = "Banco Macro";

        public static readonly List<string> Bancos = [
            BancoNacion, 
            BancoProvincia, 
            BancoGalicia, 
            BancoSantander, 
            BancoFrances, 
            BancoMacro
        ];
    }
}