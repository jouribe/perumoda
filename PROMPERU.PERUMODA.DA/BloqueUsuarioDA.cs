using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.DA
{
    public static class BloqueUsuarioDA
    {
        private static readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["BDPeruModa"].ConnectionString;

        public static void InsertarUsuario(BloqueUsuarioBE bloqueUsuarioBe)
        {
            // Cadena de conexión
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            
            // Creamos el objeto command
            SqlCommand command = new SqlCommand("USP_BloqueUsuario_INS", conexion)
            {
                // Definimos que vamos a usar un procedimiento almacenado
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                // Pasamos los parámetros
                command.Parameters.Add("@Bloq_Id", SqlDbType.Int).Value = bloqueUsuarioBe.BloqueId;
                command.Parameters.Add("@Usua_Id", SqlDbType.Int).Value = bloqueUsuarioBe.UsuarioId;

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
        
        /// <summary>
        /// Listado de todos los bloques desde la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<BloqueUsuarioBE> ListarBloquesPorUsuario(int usuarioId)
        {
            List<BloqueUsuarioBE> bloques = new List<BloqueUsuarioBE>();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_BloqueUsuario_LIS_PorUsuario", conexion)
                {
                    // Definimos que vamos a usar un procedimiento almacenado.
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    // Pasamos los parámetros
                    command.Parameters.Add("@Usua_Id", SqlDbType.Int).Value = usuarioId;
                    
                    // Abrimos la conexión
                    conexion.Open();

                    // Creamos un objeto DateReader
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        // Recorremos el lector.
                        while (dataReader.Read())
                        {
                            BloqueUsuarioBE bloque = new BloqueUsuarioBE
                            {
                                BloqueId = Convert.ToInt32(dataReader["BloqueId"]),
                                UsuarioId = Convert.ToInt32(dataReader["UsuarioId"])
                            };

                            bloques.Add(bloque);
                        }
                    }
                }
                catch (Exception)
                {
                    // Acción a tomar en caso de un error
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

            return bloques;
        }
    }
}