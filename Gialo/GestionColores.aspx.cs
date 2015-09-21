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
    public partial class GestionColores : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
                if (!IsPostBack)
                {
                    SetearBotones(grillaColores, true, true, false, true, true);
                }
            }
            catch
            {

            }
        }

        private void CargarGrilla()
        {
            DataTable tablaColores = ControladorGeneral.RecuperarTodosColores();
            grillaColores.DataSource = tablaColores;
            grillaColores.DataBind();
            SetearCaracteristicasComunes(grillaColores);
        }

        protected void grillaColores_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarColor(0, descripcion);
                e.Cancel = true;
                grillaColores.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaColores_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarColor(codigo, descripcion);
                e.Cancel = true;
                grillaColores.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}