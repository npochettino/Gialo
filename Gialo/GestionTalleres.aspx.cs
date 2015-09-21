using BibliotecaGiallo.Controladores;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gialo
{
    public partial class GestionTalleres : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrillaTiposTallers();
                if (!IsPostBack)
                {
                    SetearBotones(grillaTiposTalleres, true, true, false, true, true);
                }
            }
            catch
            {

            }
        }

       

        #region Talleres

        protected void grillaTalleres_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTalleres = sender as ASPxGridView;
                string descripcion = e.NewValues["descripcion"].ToString();
                string contacto = e.NewValues["contacto"].ToString();
                string responsable = e.NewValues["responsable"].ToString();
                ControladorGeneral.InsertarActualizarTaller(Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()), 0, descripcion, responsable, contacto);
                e.Cancel = true;
                grillaTalleres.CancelEdit();
                CargarGrillaTalleres(grillaTalleres, Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTalleres_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTalleres = sender as ASPxGridView;
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcion"].ToString();
                string contacto = e.NewValues["contacto"].ToString();
                string responsable = e.NewValues["responsable"].ToString();
                ControladorGeneral.InsertarActualizarTaller(Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()), codigo, descripcion, responsable, contacto);
                e.Cancel = true;
                grillaTalleres.CancelEdit();
                CargarGrillaTalleres(grillaTalleres, Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      

        private void CargarGrillaTalleres(ASPxGridView grillaTalleres, int codigoTipoTaller)
        {
            DataTable tablaTalleres = ControladorGeneral.RecuperarTodosTalleresPorTipoTaller(codigoTipoTaller);
            grillaTalleres.DataSource = tablaTalleres;
            grillaTalleres.DataBind();
            SetearCaracteristicasComunes(grillaTalleres);
        }

        #endregion

        #region Tipos talleres
        private void CargarGrillaTiposTallers()
        {
            DataTable tablaTiposTalleres = ControladorGeneral.RecuperarTodosTipoTalleres();
            grillaTiposTalleres.DataSource = tablaTiposTalleres;
            grillaTiposTalleres.DataBind();
            SetearCaracteristicasComunes(grillaTiposTalleres);
        }

        protected void grillaTiposTalleres_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarTipoTaller(0, descripcion);
                e.Cancel = true;
                grillaTiposTalleres.CancelEdit();
                CargarGrillaTiposTallers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTiposTalleres_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarTipoTaller(codigo, descripcion);
                e.Cancel = true;
                grillaTiposTalleres.CancelEdit();
                CargarGrillaTiposTallers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTalleres_Load(object sender, EventArgs e)
        {
            ASPxGridView grillaTalleres = sender as ASPxGridView;
            SetearBotones(grillaTalleres, true, true, true, true, true);
            CargarGrillaTalleres(grillaTalleres, Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()));
        }

        protected void grillaTalleres_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grillaTalleres = sender as ASPxGridView;
            int codigoTaller = Convert.ToInt32(e.Keys[0]);
            if (ControladorGeneral.ValidarTallerAEliminar(codigoTaller))
            {
                ControladorGeneral.EliminarTaller(codigoTaller);
                grillaTalleres.JSProperties["cpBorrar"] = "Se borró el taller correctamente";
                e.Cancel = true;
                CargarGrillaTalleres(grillaTalleres, Convert.ToInt32(grillaTalleres.GetMasterRowKeyValue()));
            }
            else
            {
                grillaTalleres.JSProperties["cpBorrar"] = "No se puede borrar un taller cuando tiene una entrega asignada";
                e.Cancel = true;
            }
        }
        #endregion

        

    }
}