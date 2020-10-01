using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public interface IBloqueUsuarioBL
    {
        /// <summary>
        /// Inserta bloque para cada usuario en la base de datos.
        /// </summary>
        /// <param name="bloqueUsuarioBe"></param>
        void InsertarBloqueUsuario(BloqueUsuarioBE bloqueUsuarioBe);

        /// <summary>
        /// Listado de todos los bloques que tiene un usuario.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        List<BloqueUsuarioBE> ListarBloquesPorUsuario(int usuarioId);
    }
}