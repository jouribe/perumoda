using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.DA
{
    public static class PaisDA
    {
        private static readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["BDPeruModa"].ConnectionString;

        /// <summary>
        /// Listado de todos los bloques desde la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<PaisBE> ListarBloques()
        {
            List<PaisBE> paises = new List<PaisBE>();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_Pais_LIS", conexion)
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
                            PaisBE pais = new PaisBE
                            {
                                PaisId = Convert.ToInt32(dataReader["PaisId"]),
                                PaisISO = dataReader["PaisISO"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["PaisISO"].ToString(),
                                PaisNombre = dataReader["PaisNombre"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["PaisNombre"].ToString()
                            };

                            paises.Add(pais);
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

            return paises;
        }
    }
}