using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public interface IEventoBL
    {
        /// <summary>
        /// Listar eventos por bloque.
        /// </summary>
        /// <param name="bloqueId"></param>
        /// <returns></returns>
        List<EventoBE> ListarEventosPorBloque(int bloqueId);
    }
}