namespace Backoffice.Views.Utils
{
    public static class DateUtils
    {
        public static DateTime? ValidarFecha(string? fecha, string formato)
        {
            if(StringUtils.StringValido(fecha))
            {
                string formatoFecha = formato;
                bool esFechaValida = DateTime.TryParseExact(
                    fecha,
                    formatoFecha,
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out DateTime fechaValida
                );

                if(esFechaValida)
                    return fechaValida;
                else
                    return null;
            }

            return null;
        }
    }
}