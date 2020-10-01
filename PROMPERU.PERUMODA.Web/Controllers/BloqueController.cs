using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.BL;

namespace PROMPERU.PERUMODA.Web.Controllers
{
    /// <summary>
    /// Bloque Controller
    /// </summary>
    public class BloqueController : BaseController
    {
        #region Public Methods

        /// <summary>
        /// Muestra la vista de bloque por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Show(string slug)
        {
            ViewBag.Style = "agenda";

            IBloqueBL bloqueBl = new BloqueBL();
            BloqueBE bloque = bloqueBl.ListarBloquePorSlug(slug);

            ViewBag.Title = bloque.BloqueNombre;

            IEventoBL eventoBl = new EventoBL();
            List<EventoBE> eventos = eventoBl.ListarEventosPorBloque(bloque.BloqueId);

            ViewBag.Eventos = eventos;

            ViewBag.Suscrito = false;

            if (!User.Identity.IsAuthenticated) return View(bloque);

            IBloqueUsuarioBL bloqueUsuarioBl = new BloqueUsuarioBL();
            List<BloqueUsuarioBE> bloquesUsuario =
                bloqueUsuarioBl.ListarBloquesPorUsuario(Convert.ToInt32(Session["UsuarioId"]));

            if (bloquesUsuario.Any() && bloquesUsuario.Any(x => x.BloqueId == bloque.BloqueId))
            {
                ViewBag.Suscrito = true;
            }


            return View(bloque);
        }

        /// <summary>
        /// Registra los bloques seleccionados por el usuario.
        /// </summary>
        /// <param name="bloqueUsuarioBe"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarBloque(BloqueUsuarioBE bloqueUsuarioBe)
        {
            if (!ModelState.IsValid) return RedirectToAction("Show", "Bloque");

            IBloqueUsuarioBL bloqueUsuarioBl = new BloqueUsuarioBL();
            bloqueUsuarioBl.InsertarBloqueUsuario(bloqueUsuarioBe);

            return Redirect(Request.UrlReferrer?.ToString());
        }

        #endregion
    }
}