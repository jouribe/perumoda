using System.Web.Mvc;

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