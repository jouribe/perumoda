using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.DA;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public class PaisBL : IPaisBL
    {
        /// <summary>
        /// Listado de paises.
        /// </summary>
        /// <returns></returns>
        public List<PaisBE> ListaPaises()
        {
            return PaisDA.ListarBloques();
        }
    }
}