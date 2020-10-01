using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.DA
{
    public static class BloqueDA
    {
        private static readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["BDPeruModa"].ConnectionString;

        /// <summary>
        /// Listado de todos los bloques desde la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<BloqueBE> ListarBloques()
        {
            List<BloqueBE> bloques = new List<BloqueBE>();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_Bloque_LIS", conexion)
                {
                    // Definimos que vamos a usar un procedimiento almacenado.
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    // Abrimos la conexión
                    conexion.Open();

                    // Creamos un objeto DateReader
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        // Recorremos el lector.
                        while (dataReader.Read())
                        {
                            BloqueBE bloque = new BloqueBE
                            {
                                BloqueId = Convert.ToInt32(dataReader["BloqueId"]),
                                BloqueNombre = dataReader["BloqueNombre"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueNombre"].ToString(),
                                BloqueNombreEN = dataReader["BloqueNombreEN"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueNombreEN"].ToString(),
                                BloqueSlug = dataReader["BloqueSlug"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueSlug"].ToString(),
                                BloqueSubtitulo = dataReader["BloqueSubtitulo"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueSubtitulo"].ToString(),
                                BloqueSubtituloEN = dataReader["BloqueSubtituloEN"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueSubtituloEN"].ToString(),
                                BloqueTransmision = dataReader["BloqueTransmision"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["BloqueTransmision"].ToString(),
                                BloqueFechaInicio = dataReader["BloqueFechaInicio"] == DBNull.Value
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dataReader["BloqueFechaInicio"]),
                                BloqueFechaFin = dataReader["BloqueFechaFin"] == DBNull.Value
                                    ? DateTime.Now
                                    : Convert.ToDateTime(dataReader["BloqueFechaFin"])
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

        /// <summary>
        /// Listar bloque pro slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public static BloqueBE ListarBloquePorSlug(string slug)
        {
            BloqueBE bloque = new BloqueBE();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_Bloque_SEL", conexion)
                {
                    // Definimos que vamos a usar un procedimiento almacenado.
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    // Pasamos los parámetros
                    command.Parameters.Add("@Bloque_Slug", SqlDbType.VarChar).Value = slug;

                    // Abrimos la conexión
                    conexion.Open();

                    // Creamos un objeto DataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bloque.BloqueId = Convert.ToInt32(reader["BloqueId"]);
                            bloque.BloqueNombre = reader["BloqueNombre"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueNombre"].ToString();
                            bloque.BloqueNombreEN = reader["BloqueNombreEN"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueNombreEN"].ToString();
                            bloque.BloqueSlug = reader["BloqueSlug"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueSlug"].ToString();
                            bloque.BloqueSubtitulo = reader["BloqueSubtitulo"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueSubtitulo"].ToString();
                            bloque.BloqueSubtituloEN = reader["BloqueSubtituloEN"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueSubtituloEN"].ToString();
                            bloque.BloqueTransmision = reader["BloqueTransmision"] == DBNull.Value
                                ? string.Empty
                                : reader["BloqueTransmision"].ToString();
                            bloque.BloqueFechaInicio = reader["BloqueFechaInicio"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["BloqueFechaInicio"]);
                            bloque.BloqueFechaFin = reader["BloqueFechaFin"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["BloqueFechaFin"]);
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

            return bloque;
        }
    }
}