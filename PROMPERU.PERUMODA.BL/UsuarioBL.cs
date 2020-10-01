using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.DA;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public class UsuarioBL : IUsuarioBL
    {
        /// <summary>
        /// Inserta usuario en la base de datos.
        /// </summary>
        /// <param name="usuario"></param>
        public void InsertarUsuario(UsuarioBE usuario)
        {
            UsuarioDA.InsertarUsuario(usuario);
        }

        /// <summary>
        /// Listar usuario por correo electrónico y número de documento.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public UsuarioBE ListarUsuario(UsuarioBE usuario)
        {
            return UsuarioDA.ListarUsuario(usuario);
        }
    }
}