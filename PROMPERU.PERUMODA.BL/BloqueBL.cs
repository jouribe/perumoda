using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.DA;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public class BloqueBL : IBloqueBL
    {
        /// <summary>
        /// Listar todos los bloques.
        /// </summary>
        /// <returns></returns>
        public List<BloqueBE> ListarBloques()
        {
            return BloqueDA.ListarBloques();
        }

        /// <summary>
        /// Lista bloque por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BloqueBE ListarBloquePorSlug(string slug)
        {
            return BloqueDA.ListarBloquePorSlug(slug);
        }
    }
}