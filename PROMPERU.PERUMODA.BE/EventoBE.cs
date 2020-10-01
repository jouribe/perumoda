using System;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BE
{
    public class EventoBE
    {
        public int EventoId { get; set; }
        public string EventoTitulo { get; set; }
        public string EventoTituloEN { get; set; }
        public string EventoExpositor { get; set; }
        public string EventoExpositorEN { get; set; }
        public DateTime EventoFecha { get; set; }
        public string EventoHoraInicio { get; set; }
        public string EventoHoraFin { get; set; }
    }
}