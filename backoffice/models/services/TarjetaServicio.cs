using MySql.Data.MySqlClient;

namespace Backoffice.Models.Services
{
    public class TarjetaServicio
    {
        private string _connectionString = "Server=localhost;Port=3306;Database=banco_db;Uid=root;";
        
        public List<Tarjeta> ObtenerTarjetas()
        {
            var tarjetas = new List<Tarjeta>();
            string query = "SELECT banco_emisor, dni_titular, estado, numero_tarjeta, num_cuenta, saldo FROM tarjetas";

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            Enum.TryParse(lector["estado"].ToString(), out EEstadoTarjeta estadoTarjeta);
                            int.TryParse(lector["num_cuenta"].ToString(), out int numeroCuenta);
                            decimal.TryParse(lector["saldo"].ToString(), out decimal saldo);

                            var tarjeta = new Tarjeta
                            {
                                Banco = lector["banco_emisor"].ToString() ?? "",
                                DniTitular = lector["dni_titular"].ToString() ?? "",
                                Estado = estadoTarjeta,
                                NumeroTarjeta = lector["numero_tarjeta"].ToString() ?? "",
                                NumeroCuenta = numeroCuenta,
                                Saldo = saldo
                            };

                            tarjetas.Add(tarjeta);
                        }
                    }
                }
            }
            return tarjetas;
        }

        // public UsuarioTarjeta ObtenerDetalleTarjeta(int numeroCuenta)
        // {
            
        // }
    }
}