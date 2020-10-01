using System;

// ReSharper disable InconsistentNaming

namespace PROMPERU.PERUMODA.BE
{
    public class BloqueBE
    {
        public int BloqueId { get; set; }
        public string BloqueNombre { get; set; }
        public string BloqueSlug { get; set; }
        public string BloqueNombreEN { get; set; }
        public string BloqueSubtitulo { get; set; }
        public string BloqueSubtituloEN { get; set; }
        public string BloqueTransmision { get; set; }
        public DateTime BloqueFechaInicio { get; set; }
        public DateTime BloqueFechaFin { get; set; }
    }
}