using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public interface IPaisBL
    {
        /// <summary>
        /// Listado de paises.
        /// </summary>
        /// <returns></returns>
        List<PaisBE> ListaPaises();
    }
}