using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.BL;
using PROMPERU.PERUMODA.Web.Managers;

namespace PROMPERU.PERUMODA.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Public Methods

        /// <summary>
        /// Override method to support languages.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            HttpCookie langCookie = Request.Cookies["culture"];

            string lang;

            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                string[] userLanguage = Request.UserLanguages;
                string userLang = userLanguage != null ? userLanguage[0] : "";


                lang = userLang != "" ? userLang : LanguageManager.GetDefaultLanguage();
            }

            LanguageManager.SetLanguage(lang);

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// Change language.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ActionResult ChangeLanguage(string lang)
        {
            LanguageManager.SetLanguage(lang);

            return Redirect(Request.UrlReferrer?.ToString());
        }

        /// <summary>
        /// Show bloques partial view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Bloques(int id = 0)
        {
            IBloqueBL bloqueBl = new BloqueBL();
            List<BloqueBE> bloques = bloqueBl.ListarBloques();

            if (id != 0)
            {
                bloques.RemoveAll(x => x.BloqueId == id);
            }

            return PartialView("_Bloques", bloques);
        }

        #endregion
    }
}