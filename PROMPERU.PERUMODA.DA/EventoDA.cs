using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.DA
{
    public static class EventoDA
    {
        private static readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["BDPeruModa"].ConnectionString;

        /// <summary>
        /// Listado de todos los bloques desde la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<EventoBE> ListarEventoPorBloque(int bloqueId)
        {
            List<EventoBE> eventos = new List<EventoBE>();

            // Conexión a la BD
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Creamos un objeto command
                SqlCommand command = new SqlCommand("USP_Evento_LIS_PorId", conexion)
                {
                    // Definimos que vamos a usar un procedimiento almacenado.
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    // Pasamos los parámetros
                    command.Parameters.Add("@Bolq_Id", SqlDbType.Int).Value = bloqueId;
                    
                    // Abrimos la conexión
                    conexion.Open();

                    // Creamos un objeto DateReader
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        // Recorremos el lector.
                        while (dataReader.Read())
                        {
                            EventoBE evento = new EventoBE
                            {
                                EventoId = Convert.ToInt32(dataReader["EventoId"]),
                                EventoTitulo = dataReader["EventoTitulo"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoTitulo"].ToString(),
                                EventoTituloEN = dataReader["EventoTituloEN"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoTituloEN"].ToString(),
                                EventoExpositor = dataReader["EventoExpositor"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoExpositor"].ToString(),
                                EventoExpositorEN = dataReader["EventoExpositorEN"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoExpositorEN"].ToString(),
                                EventoFecha = dataReader["EventoFecha"] == DBNull.Value
                                    ? DateTime.Now.Date
                                    : Convert.ToDateTime(dataReader["EventoFecha"]).Date,
                                EventoHoraInicio = dataReader["EventoHoraInicio"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoHoraInicio"].ToString(),
                                EventoHoraFin = dataReader["EventoHoraFin"] == DBNull.Value
                                    ? string.Empty
                                    : dataReader["EventoHoraFin"].ToString(),
                            };

                            eventos.Add(evento);
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

            return eventos;
        }
    }
}