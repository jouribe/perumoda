using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.BL;
using PROMPERU.PERUMODA.Web.Models;

namespace PROMPERU.PERUMODA.Web.Controllers
{
    public class UsuarioController : BaseController
    {
        #region Private Properties

        /// <summary>
        /// Lista de tipo de usuario.
        /// </summary>
        private static readonly IEnumerable<UsuarioTipo> TipoUsuarios = new List<UsuarioTipo>
        {
            new UsuarioTipo
            {
                TipoUsuarioId = "Empresario Exportador / Potencial Exportador",
                TipoUsuarioDescripcion = "Empresario Exportador / Potencial Exportador"
            },
            new UsuarioTipo
            {
                TipoUsuarioId = "Emprendedor en el sector de la Industria de la Vestimenta y Decoración",
                TipoUsuarioDescripcion = "Emprendedor en el sector de la Industria de la Vestimenta y Decoración"
            },
            new UsuarioTipo
            {
                TipoUsuarioId = "Estudiante",
                TipoUsuarioDescripcion = "Estudiante"
            }
        };

        /// <summary>
        /// Lista de tipo de documento.
        /// </summary>
        private static readonly IEnumerable<UsuarioTipoDocumento> TipoDocumentos = new List<UsuarioTipoDocumento>
        {
            new UsuarioTipoDocumento
            {
                UsuarioTipoDocumentoId = "DNI",
                UsuarioTipoDocumentoDescripcion = "DNI"
            },
            new UsuarioTipoDocumento
            {
                UsuarioTipoDocumentoId = "CE",
                UsuarioTipoDocumentoDescripcion = "CE"
            },
            new UsuarioTipoDocumento
            {
                UsuarioTipoDocumentoId = "RUC",
                UsuarioTipoDocumentoDescripcion = "RUC"
            },
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Muestra la vista para el registro de usuario.
        /// </summary>
        /// <returns></returns>
        public ActionResult Registro()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TipoUsuarios = TipoUsuarios;
            ViewBag.TipoDocumentos = TipoDocumentos;

            IBloqueBL bloqueBl = new BloqueBL();
            List<BloqueBE> bloques = bloqueBl.ListarBloques();

            ViewBag.Bloques = bloques;

            IPaisBL paisBl = new PaisBL();
            List<PaisBE> paises = paisBl.ListaPaises();

            ViewBag.Paises = paises;

            return View();
        }

        /// <summary>
        /// Registra un usuario en la base de datos.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid) return View();

            UsuarioBE usuarioBe = new UsuarioBE
            {
                UsuarioNombres = usuario.Nombres,
                UsuarioApellidos = usuario.Apellidos,
                UsuarioCorreoElectronico = usuario.CorreoElectronico,
                UsuarioTipoDocumento = usuario.TipoDocumento,
                UsuarioNumeroDocumento = usuario.NumeroDocumento,
                UsuarioTelefono = usuario.NumeroTelefono,
                UsuarioRazonSocial = usuario.RazonSocial,
                UsuarioPaisId = usuario.Pais,
                UsuarioRegion = usuario.Region,
                UsuarioTipo = usuario.TipoUsuario,
                UsuarioAutorizoCompartirDatos = !string.IsNullOrEmpty(usuario.AutorizoCompartir)
            };

            IUsuarioBL usuarioBl = new UsuarioBL();
            usuarioBl.InsertarUsuario(usuarioBe);
            UsuarioBE usuarioAutenticado = usuarioBl.ListarUsuario(usuarioBe);

            Session["UsuarioId"] = usuarioAutenticado.UsuarioId;
            Session["UsuarioNombre"] = usuarioAutenticado.UsuarioNombres;
            Session["UsuarioTipo"] = usuarioAutenticado.UsuarioTipo;
            FormsAuthentication.SetAuthCookie(usuarioAutenticado.UsuarioCorreoElectronico, false);

            foreach (int usuarioBloque in usuario.Bloques)
            {
                BloqueUsuarioBE bloqueUsuarioBe = new BloqueUsuarioBE
                {
                    BloqueId = usuarioBloque,
                    UsuarioId = usuarioAutenticado.UsuarioId
                };

                IBloqueUsuarioBL bloqueUsuarioBl = new BloqueUsuarioBL();
                bloqueUsuarioBl.InsertarBloqueUsuario(bloqueUsuarioBe);
            }

            return RedirectToAction("Gracias", "Usuario");

        }

        /// <summary>
        /// Muestra la vista para el login de usuario.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Inicia sesión de usuario al sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioBE usuario)
        {
            if (ModelState.IsValid)
            {
                IUsuarioBL usuarioBl = new UsuarioBL();
                UsuarioBE usuarioAutenticado = usuarioBl.ListarUsuario(usuario);

                if (!string.IsNullOrEmpty(usuarioAutenticado.UsuarioCorreoElectronico) && !string.IsNullOrEmpty(usuarioAutenticado.UsuarioNumeroDocumento))
                {
                    Session["UsuarioId"] = usuarioAutenticado.UsuarioId;
                    Session["UsuarioNombre"] = usuarioAutenticado.UsuarioNombres;
                    Session["UsuarioTipo"] = usuarioAutenticado.UsuarioTipo;
                    FormsAuthentication.SetAuthCookie(usuarioAutenticado.UsuarioCorreoElectronico, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", @"Usuario inválido");

            return View();
        }

        /// <summary>
        /// Cierra sesión de usuario.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["UsuarioId"] = null;
            Session["UsuarioNombre"] = null;
            Session["UsuarioTipo"] = null;

            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Muestra la vista de gracias después de que el usuario se registra.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Gracias()
        {
            return View();
        }

        #endregion

    }
}