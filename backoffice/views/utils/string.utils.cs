namespace Backoffice.Views.Utils
{
    public static class StringUtils
    {
        public static bool StringValido(string? valor)
        {
            if(string.IsNullOrEmpty(valor))
                return false;
            else
                return true;
        }
    }
}