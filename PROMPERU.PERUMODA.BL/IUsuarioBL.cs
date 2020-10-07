using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public interface IUsuarioBL
    {
        /// <summary>
        /// Inserta usuario en la base de datos.
        /// </summary>
        /// <param name="usuario"></param>
        void InsertarUsuario(UsuarioBE usuario);

        /// <summary>
        /// Listar usuario por correo electrónico y número de documento.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        UsuarioBE ListarUsuario(UsuarioBE usuario);

        /// <summary>
        /// Listar los usuarios registrados en la base de datos con los bloques a los que suscribieron.
        /// </summary>
        /// <returns></returns>
        List<UsuarioBloqueBE> ListarUsuarioBloque();
    }
}