using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Listar los usuarios registrados en la base de datos con los bloques a los que suscribieron.
        /// </summary>
        /// <returns></returns>
        public List<UsuarioBloqueBE> ListarUsuarioBloque()
        {
            return UsuarioDA.ListarUsuarioBloque()
                .GroupBy(x => new
                {
                    x.UsuarioId, 
                    x.UsuarioNombres, 
                    x.UsuarioApellidos, 
                    x.UsuarioCorreoElectronico, 
                    x.UsuarioRazonSocial,
                    x.UsuarioTipoDocumento, 
                    x.UsuarioNumeroDocumento,
                    x.UsuarioTelefono,
                    x.PaisNombre,
                    x.UsuarioRegion,
                    x.UsuarioTipo,
                    x.UsuarioAutorizoCompartirDatos,
                    x.UsuarioFechaCreacion
                })
                .Select(g => new UsuarioBloqueBE
                {
                    UsuarioId = g.Key.UsuarioId,
                    UsuarioNombres = g.Key.UsuarioNombres,
                    UsuarioApellidos = g.Key.UsuarioApellidos,
                    UsuarioCorreoElectronico = g.Key.UsuarioCorreoElectronico,
                    UsuarioRazonSocial = g.Key.UsuarioRazonSocial,
                    UsuarioTipoDocumento = g.Key.UsuarioTipoDocumento,
                    UsuarioNumeroDocumento = g.Key.UsuarioNumeroDocumento,
                    UsuarioTelefono = g.Key.UsuarioTelefono,
                    PaisNombre = g.Key.PaisNombre,
                    UsuarioRegion = g.Key.UsuarioRegion,
                    UsuarioTipo = g.Key.UsuarioTipo,
                    UsuarioAutorizoCompartirDatos = g.Key.UsuarioAutorizoCompartirDatos,
                    UsuarioFechaCreacion = g.Key.UsuarioFechaCreacion,
                    BloqueNombre = string.Join(", ", g.Select(i => i.BloqueNombre))
                })
                .ToList();
        }
    }
}