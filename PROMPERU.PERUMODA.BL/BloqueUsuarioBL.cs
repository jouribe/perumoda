using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.DA;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public class BloqueUsuarioBL : IBloqueUsuarioBL
    {
        /// <summary>
        /// Inserta bloque para cada usuario en la base de datos.
        /// </summary>
        /// <param name="bloqueUsuarioBe"></param>
        public void InsertarBloqueUsuario(BloqueUsuarioBE bloqueUsuarioBe)
        {
            BloqueUsuarioDA.InsertarUsuario(bloqueUsuarioBe);
        }

        /// <summary>
        /// Listado de todos los bloques que tiene un usuario.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public List<BloqueUsuarioBE> ListarBloquesPorUsuario(int usuarioId)
        {
            return BloqueUsuarioDA.ListarBloquesPorUsuario(usuarioId);
        }
    }
}