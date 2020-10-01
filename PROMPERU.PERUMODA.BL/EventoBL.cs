using System.Collections.Generic;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.DA;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BL
{
    public class EventoBL : IEventoBL
    {
        /// <summary>
        /// Listar eventos por bloque.
        /// </summary>
        /// <param name="bloqueId"></param>
        /// <returns></returns>
        public List<EventoBE> ListarEventosPorBloque(int bloqueId)
        {
            return EventoDA.ListarEventoPorBloque(bloqueId);
        }
    }
}