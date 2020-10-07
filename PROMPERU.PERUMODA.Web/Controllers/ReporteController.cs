using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using PROMPERU.PERUMODA.BE;
using PROMPERU.PERUMODA.BL;

namespace PROMPERU.PERUMODA.Web.Controllers
{
    [Authorize]
    public class ReporteController : BaseController
    {
        #region Private Methods

        private static DataTable ConvertToDataTable<T>(List<T> model)
        {
            // Creamos una instancia del DataTable
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Obtenemos todas las propiedades del modelo
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Agregamos los nombre de columnas a nuestro DataTable
            foreach (PropertyInfo prop in props)
            {
                // Configuramos el nombre de la columna como los nombres de las propiedades
                dataTable.Columns.Add(prop.Name);
            }

            // Agregamos filas y sus valores al DataTable
            foreach (T item in model)
            {
                object[] values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    // Insertamos los valores de las propiedades a la fila
                    values[i] = props[i].GetValue(item, null);
                }

                // Finalmente agregamos el valor al DataTable
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Mostrar reporte de usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int size = 15)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (User.Identity.IsAuthenticated && Session["UsuarioTipo"] != null &&
                Session["UsuarioTipo"].ToString().ToLower() != "administrador")
                return RedirectToAction("Index", "Home");

            IUsuarioBL usuarioBl = new UsuarioBL();
            List<UsuarioBloqueBE> usuarios = usuarioBl.ListarUsuarioBloque();

            ViewBag.Total = usuarios.Count;
            ViewBag.CurrentPage = page;
            ViewBag.NextPage = page + 1;
            ViewBag.Pages = usuarios.Count / size;
            ViewBag.Usuarios = usuarios.Skip((page - 1) * size).Take(size).ToList();

            return View();
        }

        /// <summary>
        /// Genera el excel con la data de usuarios desde la base de datos.
        /// </summary>
        public ActionResult ExportarExcel()
        {
            if (!User.Identity.IsAuthenticated || Session["UsuarioTipo"].ToString().ToLower() != "administrador")
                return RedirectToAction("Index", "Reporte");

            IUsuarioBL usuarioBl = new UsuarioBL();
            List<UsuarioBloqueBE> usuarios = usuarioBl.ListarUsuarioBloque();

            GridView gv = new GridView {DataSource = ConvertToDataTable(usuarios)};
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=Usuarios-{DateTime.Now:ddMMyyyy}.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index", "Reporte");
        }

        #endregion
    }
}