// ReSharper disable InconsistentNaming

using System;

namespace PROMPERU.PERUMODA.BE
{
    public class UsuarioBE
    {
        public int UsuarioId { get; set; }
        public string UsuarioNombres { get; set; }
        public string UsuarioApellidos { get; set; }
        public string UsuarioCorreoElectronico { get; set; }
        public string UsuarioRazonSocial { get; set; }
        public string UsuarioTipoDocumento { get; set; }
        public string UsuarioNumeroDocumento { get; set; }
        public string UsuarioTelefono { get; set; }
        public int UsuarioPaisId { get; set; }
        public string UsuarioRegion { get; set; }
        public string UsuarioTipo { get; set; }
        public bool UsuarioAutorizoCompartirDatos { get; set; }
        public DateTime UsuarioFechaCreacion { get; set; }
        public DateTime UsuarioFechaEdicion { get; set; }
        public DateTime UsuarioFechaEliminacion { get; set; }
    }
}