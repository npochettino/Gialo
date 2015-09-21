using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaGiallo.Controladores;

namespace Gialo
{
    public partial class GestionArticulos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
                if (!IsPostBack)
                {
                    SetearBotones(grillaArticulos, true, true, true, true, true);
                }
            }
            catch
            {

            }
        }

        private void CargarGrilla()
        {
            DataTable tablaArticulos = ControladorGeneral.RecuperarTodosArticulos();
            grillaArticulos.DataSource = tablaArticulos;
            grillaArticulos.DataBind();
            SetearCaracteristicasComunes(grillaArticulos);
        }

        protected void grillaArticulos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarArticulo(0, descripcion);
                e.Cancel = true;
                grillaArticulos.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaArticulos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarArticulo(codigo, descripcion);
                e.Cancel = true;
                grillaArticulos.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaArticulos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grillaArticulos = sender as ASPxGridView;
            int codigoArticulo = Convert.ToInt32(e.Keys[0]);
            if (ControladorGeneral.ValidarArticuloAEliminar(codigoArticulo))
            {
                ControladorGeneral.EliminarArticulo(codigoArticulo);
                grillaArticulos.JSProperties["cpBorrar"] = "Se borró el artículo correctamente";
                e.Cancel = true;
                CargarGrilla();
            }
            else
            {
                grillaArticulos.JSProperties["cpBorrar"] = "No se puede borrar un artículo que pertenece a una tanda";
                e.Cancel = true;
            }
        }
    }
}