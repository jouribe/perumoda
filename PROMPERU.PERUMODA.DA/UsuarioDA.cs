using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.DA
{
    public static class UsuarioDA
    {
        private static readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["BDPeruModa"].ConnectionString;

        public static void InsertarUsuario(UsuarioBE usuario)
        {
            // Cadena de conexión
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            
            // Creamos el objeto command
            SqlCommand command = new SqlCommand("USP_Usuario_INS", conexion)
            {
                // Definimos que vamos a usar un procedimiento almacenado
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                // Pasamos los parámetros
                command.Parameters.Add("@Usua_Nombres", SqlDbType.VarChar).Value = usuario.UsuarioNombres;
                command.Parameters.Add("@Usua_Apellidos", SqlDbType.VarChar).Value = usuario.UsuarioApellidos;
                command.Parameters.Add("@Usua_CorreoElectronico", SqlDbType.VarChar).Value = usuario.UsuarioCorreoElectronico;
                command.Parameters.Add("@Usua_TipoDocumento", SqlDbType.VarChar).Value = usuario.UsuarioTipoDocumento;
                command.Parameters.Add("@Usua_NumeroDocumento", SqlDbType.VarChar).Value = usuario.UsuarioNumeroDocumento;
                command.Parameters.Add("@Usua_Telefono", SqlDbType.VarChar).Value = usuario.UsuarioTelefono;
                command.Parameters.Add("@Pais_Id", SqlDbType.Int).Value = usuario.UsuarioPaisId;
                command.Parameters.Add("@Usua_Region", SqlDbType.VarChar).Value = usuario.UsuarioRegion;
                command.Parameters.Add("@Usua_Tipo", SqlDbType.VarChar).Value = usuario.UsuarioTipo;
                command.Parameters.Add("@Usua_AutorizoCompartirDatos", SqlDbType.Bit).Value = usuario.UsuarioAutorizoCompartirDatos;
                command.Parameters.Add("@Usua_RazonSocial", SqlDbType.VarChar).Value = usuario.UsuarioRazonSocial;

                // Abrimos la conexion
                conexion.Open();

                // Ejecutamos la ejecución del procedimiento en la base de datos.
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //Acción a tomar en caso de un error
            }
            finally
            {
                // Nos aseguramos de cerrar la conexión que hemos abierto
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                    conexion.Dispose();
                    command.Dispose();
                }
            }
        }

        public static UsuarioBE ListarUsuario(UsuarioBE usuario)
        {
            UsuarioBE usuarioBe = new UsuarioBE();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_Usuario_SEL", conexion)
                {
                    // Definimos que vamos a usar un procedimiento almacenado.
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    // Pasamos los parámetros
                    command.Parameters.Add("@Usua_CorreoElectornico", SqlDbType.VarChar).Value = usuario.UsuarioCorreoElectronico;
                    command.Parameters.Add("@Usua_NumeroDocumento", SqlDbType.VarChar).Value = usuario.UsuarioNumeroDocumento;

                    // Abrimos la conexión
                    conexion.Open();

                    // Creamos un objeto DataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarioBe.UsuarioId = Convert.ToInt32(reader["UsuarioId"]);
                            usuarioBe.UsuarioNombres = reader["UsuarioNombres"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioNombres"].ToString();
                            usuarioBe.UsuarioApellidos = reader["UsuarioApellidos"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioApellidos"].ToString();
                            usuarioBe.UsuarioCorreoElectronico = reader["UsuarioCorreoElectronico"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioCorreoElectronico"].ToString();
                            usuarioBe.UsuarioRazonSocial = reader["UsuarioRazonSocial"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioRazonSocial"].ToString();
                            usuarioBe.UsuarioTipoDocumento = reader["UsuarioTipoDocumento"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioTipoDocumento"].ToString();
                            usuarioBe.UsuarioNumeroDocumento = reader["UsuarioNumeroDocumento"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioNumeroDocumento"].ToString();
                            usuarioBe.UsuarioTipo = reader["UsuarioTipo"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioTipo"].ToString();
                            usuarioBe.UsuarioAutorizoCompartirDatos = Convert.ToBoolean(reader["UsuarioTipo"]);
                            usuarioBe.UsuarioPaisId = Convert.ToInt32(reader["UsuarioPaisId"]);
                            usuarioBe.UsuarioRegion = reader["UsuarioRegion"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioRegion"].ToString();
                            usuarioBe.UsuarioTelefono = reader["UsuarioTelefono"] == DBNull.Value
                                ? string.Empty
                                : reader["UsuarioTelefono"].ToString();
                            usuarioBe.UsuarioFechaCreacion = reader["UsuarioFechaCreacion"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["UsuarioFechaCreacion"]);
                            usuarioBe.UsuarioFechaEdicion = reader["UsuarioFechaEdicion"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["UsuarioFechaEdicion"]);
                            usuarioBe.UsuarioFechaEliminacion = reader["UsuarioFechaEliminacion"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["UsuarioFechaEliminacion"]);
                        }
                    }
                }
                catch (Exception)
                {
                    //Acción a tomar en caso de un error
                }
                finally
                {
                    // Nos aseguramos de cerrar la conexión que hemos abierto
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                        conexion.Dispose();
                        command.Dispose();
                    }
                }
            }

            return usuarioBe;
        }
    }
}