using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.BL;
using PROMPERU.PERUMODA.Web.Managers;

namespace PROMPERU.PERUMODA.Web.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : BaseController
    {
        #region Public Mehtods

        /// <summary>
        /// Show home page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Style = "agenda home";
            ViewBag.Home = true;

            return View();
        }

        #endregion
    }
}