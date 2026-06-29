using MySql.Data.MySqlClient;

namespace Backoffice.Models.Services
{
    public class LiquidacionServicio
    {
        private string _connectionString = "Server=localhost;Port=3306;Database=banco_db;Uid=root;";

        public bool AgregarLiquidacion(Liquidacion liquidacion)
        {
            try
            {
                string query = @"INSERT INTO liquidaciones (num_cuenta, periodo, fecha_vencimiento, total_a_pagar, pago_minimo) 
                                VALUES (@num_cuenta, @periodo, @fecha_vencimiento, @total_a_pagar, @pago_minimo)";
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@num_cuenta", liquidacion.NumeroCuenta);
                        comando.Parameters.AddWithValue("@periodo", liquidacion.Periodo);
                        comando.Parameters.AddWithValue("@fecha_vencimiento", liquidacion.FechaVencimiento);
                        comando.Parameters.AddWithValue("@total_a_pagar", liquidacion.TotalPagar);
                        comando.Parameters.AddWithValue("@pago_minimo", liquidacion.PagoMinimo);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        if(filasAfectadas > 0)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}