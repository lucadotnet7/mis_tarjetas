using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Prng;

namespace Backoffice.Models.Services
{
    public class UsuarioServicio
    {
        private string _connectionString = "Server=localhost;Port=3306;Database=banco_db;Uid=root;";
        
        public Usuario? ObtenerUsuarioPorDocumento(string documento)
        {
            Usuario? usuario = null;
            string query = @"SELECT documento, tipo_doc, nombre, apellido, fecha_nacimiento, 
                                email, usuario, password, creado_el 
                                FROM usuarios WHERE documento = @documento";
            try
            {
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using(var comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@documento", documento);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while(lector.Read())
                            {
                                Enum.TryParse(lector["tipo_doc"].ToString(), out ETipoDocumento tpDocumento);
                                
                                usuario = new Usuario
                                {
                                    Documento = lector["documento"].ToString() ?? "",
                                    TipoDocumento = tpDocumento,
                                    Nombre = lector["nombre"].ToString() ?? "",
                                    Apellido = lector["apellido"].ToString() ?? "",
                                    FechaNacimiento = (DateTime)lector["fecha_nacimiento"],
                                    CorreoElectronico = lector["email"].ToString() ?? "",
                                    NombreUsuario = lector["usuario"].ToString() ?? "",
                                    Contrasenia = lector["password"].ToString() ?? "",
                                };
                                return usuario;
                            }
                            return usuario;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public Usuario? ObtenerUsuarioPorCorreoElectronico(string correo)
        {
            Usuario? usuario = null;
            string query = @"SELECT documento, tipo_doc, nombre, apellido, fecha_nacimiento, 
                                email, usuario, password, creado_el 
                                FROM usuarios WHERE email = @correo";
            try
            {
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using(var comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@correo", correo);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while(lector.Read())
                            {
                                Enum.TryParse(lector["tipo_doc"].ToString(), out ETipoDocumento tpDocumento);
                                
                                usuario = new Usuario
                                {
                                    Documento = lector["documento"].ToString() ?? "",
                                    TipoDocumento = tpDocumento,
                                    Nombre = lector["nombre"].ToString() ?? "",
                                    Apellido = lector["apellido"].ToString() ?? "",
                                    FechaNacimiento = (DateTime)lector["fecha_nacimiento"],
                                    CorreoElectronico = lector["email"].ToString() ?? "",
                                    NombreUsuario = lector["usuario"].ToString() ?? "",
                                    Contrasenia = lector["password"].ToString() ?? "",
                                };
                                return usuario;
                            }
                            return usuario;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        public List<Usuario> ObtenerUsuarios()
        {
            var usuarios = new List<Usuario>();
            string query = "SELECT documento, tipo_doc, nombre, apellido, fecha_nacimiento, email, usuario, password, creado_el FROM usuarios";

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            Enum.TryParse(lector["tipo_doc"].ToString(), out ETipoDocumento tpDocumento);
                            
                            var usuario = new Usuario
                            {
                                Documento = lector["documento"].ToString() ?? "",
                                TipoDocumento = tpDocumento,
                                Nombre = lector["nombre"].ToString() ?? "",
                                Apellido = lector["apellido"].ToString() ?? "",
                                FechaNacimiento = (DateTime)lector["fecha_nacimiento"],
                                CorreoElectronico = lector["email"].ToString() ?? "",
                                NombreUsuario = lector["usuario"].ToString() ?? "",
                                Contrasenia = lector["password"].ToString() ?? "",
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            try
            {
                string query = @"INSERT INTO usuarios (nombre, apellido, fecha_nacimiento, documento, tipo_doc, email, creado_el) 
                                VALUES (@nombre, @apellido, @fecha_nacimiento, @documento, @tipo_doc, @email, @creado_el)";
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        comando.Parameters.AddWithValue("@fecha_nacimiento", usuario.FechaNacimiento);
                        comando.Parameters.AddWithValue("@documento", usuario.Documento);
                        comando.Parameters.AddWithValue("@tipo_doc", usuario.TipoDocumento.ToString());
                        comando.Parameters.AddWithValue("@email", usuario.CorreoElectronico);
                        comando.Parameters.AddWithValue("@creado_el", DateTime.Now);

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

        public bool EliminarUsuario(string documento)
        {
            try
            {
                string query = @"DELETE FROM usuarios WHERE documento = @documento";
                using (var conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@documento", documento);

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