using MySql.Data.MySqlClient;

namespace Backoffice.Models.Services
{
    public class TarjetaServicio
    {
        private string _connectionString = "Server=localhost;Port=3306;Database=banco_db;Uid=root;";
        
        public Tarjeta? ObtenerTarjetaPorNumeroTarjeta(string numTarjeta)
        {
            Tarjeta? tarjeta = null;
            string query = @"SELECT num_cuenta, numero_tarjeta, banco_emisor, estado, saldo, dni_titular
                                FROM tarjetas WHERE numero_tarjeta = @numTarjeta";
            try
            {
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using(var comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@numero_tarjeta", numTarjeta);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while(lector.Read())
                            {
                                Enum.TryParse(lector["estado"].ToString(), out EEstadoTarjeta estadoTarjeta);
                                int.TryParse(lector["num_cuenta"].ToString(), out int numeroCuenta);
                                decimal.TryParse(lector["saldo"].ToString(), out decimal saldo);

                                tarjeta = new Tarjeta
                                {
                                    Banco = lector["banco_emisor"].ToString() ?? "",
                                    DniTitular = lector["dni_titular"].ToString() ?? "",
                                    Estado = estadoTarjeta,
                                    NumeroTarjeta = lector["numero_tarjeta"].ToString() ?? "",
                                    NumeroCuenta = numeroCuenta,
                                    Saldo = saldo
                                };
                                return tarjeta;
                            }
                            return tarjeta;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public Tarjeta? ObtenerTarjetaPorNumeroCuenta(int numCuenta)
        {
            Tarjeta? tarjeta = null;
            string query = @"SELECT num_cuenta, numero_tarjeta, banco_emisor, estado, saldo, dni_titular
                                FROM tarjetas WHERE num_cuenta = @numCuenta";
            try
            {
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using(var comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@numCuenta", numCuenta);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while(lector.Read())
                            {
                                Enum.TryParse(lector["estado"].ToString(), out EEstadoTarjeta estadoTarjeta);
                                int.TryParse(lector["num_cuenta"].ToString(), out int numeroCuenta);
                                decimal.TryParse(lector["saldo"].ToString(), out decimal saldo);

                                tarjeta = new Tarjeta
                                {
                                    Banco = lector["banco_emisor"].ToString() ?? "",
                                    DniTitular = lector["dni_titular"].ToString() ?? "",
                                    Estado = estadoTarjeta,
                                    NumeroTarjeta = lector["numero_tarjeta"].ToString() ?? "",
                                    NumeroCuenta = numeroCuenta,
                                    Saldo = saldo
                                };
                                return tarjeta;
                            }
                            return tarjeta;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

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

        public bool AgregarTarjeta(Tarjeta tarjeta)
        {
            try
            {
                string query = @"INSERT INTO tarjetas (numero_tarjeta, banco_emisor, estado, saldo, dni_titular) 
                                VALUES (@numero_tarjeta, @banco_emisor, @estado, @saldo, @dni_titular)";
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@numero_tarjeta", tarjeta.NumeroTarjeta);
                        comando.Parameters.AddWithValue("@banco_emisor", tarjeta.Banco);
                        comando.Parameters.AddWithValue("@estado", tarjeta.Estado.ToString());
                        comando.Parameters.AddWithValue("@saldo", tarjeta.Saldo);
                        comando.Parameters.AddWithValue("@dni_titular", tarjeta.DniTitular);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        if(filasAfectadas > 0)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public List<UsuarioTarjeta> ObtenerDetalleTarjeta(int numCuenta)
        {
            var usuariosTarjetas = new List<UsuarioTarjeta>();
            string query = @"SELECT 
                            u.nombre, u.apellido, u.email, u.documento, t.num_cuenta, t.numero_tarjeta, t.saldo, t.estado
                            FROM tarjetas t
                            INNER JOIN usuarios u ON u.documento = t.dni_titular
                            WHERE num_cuenta = @numCuenta";

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@numCuenta", numCuenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            Enum.TryParse(lector["estado"].ToString(), out EEstadoTarjeta estadoTarjeta);
                            int.TryParse(lector["num_cuenta"].ToString(), out int numeroCuenta);
                            decimal.TryParse(lector["saldo"].ToString(), out decimal saldo);

                            var usuarioTarjeta = new UsuarioTarjeta
                            {
                                Nombre = lector["nombre"].ToString() ?? "",
                                Apellido = lector["apellido"].ToString() ?? "",
                                CorreoElectronico = lector["email"].ToString() ?? "",
                                Documento = lector["documento"].ToString() ?? "",
                                NumeroCuenta = numeroCuenta,
                                EstadoTarjeta = estadoTarjeta,
                                NumeroTarjeta = lector["numero_tarjeta"].ToString() ?? "",
                                Saldo = saldo
                            };

                            usuariosTarjetas.Add(usuarioTarjeta);
                        }
                    }
                }
            }
            return usuariosTarjetas;
        }

        public bool EliminarTarjeta(int numCuenta)
        {
            try
            {
                string query = @"DELETE FROM tarjetas WHERE num_cuenta = @numCuenta";
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@numCuenta", numCuenta);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        if(filasAfectadas > 0)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}