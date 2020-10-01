using System.Collections.Generic;
using System.Threading.Tasks;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public interface IBloqueBL
    {
        /// <summary>
        /// Listar todos los bloques.
        /// </summary>
        /// <returns></returns>
        List<BloqueBE> ListarBloques();

        /// <summary>
        /// Lista bloque por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        BloqueBE ListarBloquePorSlug(string slug);
    }
}