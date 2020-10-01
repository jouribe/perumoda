using System.Collections.Generic;

namespace PROMPERU.PERUMODA.Web.Models
{
    public class Usuario
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroTelefono { get; set; }
        public string RazonSocial { get; set; }
        public int Pais { get; set; }
        public string Region { get; set; }
        public string TipoUsuario { get; set; }
        public string AutorizoCompartir { get; set; }
        public IList<int> Bloques { get; set; }
    }
}