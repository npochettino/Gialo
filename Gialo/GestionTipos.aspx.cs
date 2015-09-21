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
    public partial class GestionTipos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
                if (!IsPostBack)
                {
                    SetearBotones(grillaTipos, true, true, false, true, true);
                }
            }
            catch
            {

            }
        }

        private void CargarGrilla()
        {
            DataTable tablaTipos = ControladorGeneral.RecuperarTodosTipos();
            grillaTipos.DataSource = tablaTipos;
            grillaTipos.DataBind();
            SetearCaracteristicasComunes(grillaTipos);
        }

        #region Tipos
        protected void grillaTipos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarTipo(0, descripcion);
                e.Cancel = true;
                grillaTipos.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTipos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcion"].ToString();
                ControladorGeneral.InsertarActualizarTipo(codigo, descripcion);
                e.Cancel = true;
                grillaTipos.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Talles
        protected void grillaTalles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTalles = sender as ASPxGridView;
                string descripcion = e.NewValues["descripcionTalle"].ToString();
                ControladorGeneral.InsertarActualizarTalle(Convert.ToInt32(grillaTalles.GetMasterRowKeyValue()), 0, descripcion);
                e.Cancel = true;
                grillaTalles.CancelEdit();
                CargarGrillaTalles(grillaTalles, Convert.ToInt32(grillaTalles.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTalles_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTalles = sender as ASPxGridView;
                int codigo = Convert.ToInt32(e.Keys[0]);
                string descripcion = e.NewValues["descripcionTalle"].ToString();
                ControladorGeneral.InsertarActualizarTalle(Convert.ToInt32(grillaTalles.GetMasterRowKeyValue()),codigo, descripcion);
                e.Cancel = true;
                grillaTalles.CancelEdit();
                CargarGrillaTalles(grillaTalles, Convert.ToInt32(grillaTalles.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTalles_Load(object sender, EventArgs e)
        {
            ASPxGridView grillaTalles = sender as ASPxGridView;
            SetearBotones(grillaTalles, true, true, false, true, true);
            CargarGrillaTalles(grillaTalles, Convert.ToInt32(grillaTalles.GetMasterRowKeyValue()));
            //SetearCaracteristicasComunes(grillaTalles);
        }

        private void CargarGrillaTalles(ASPxGridView grillaTalles, int codigoTipo)
        {
            DataTable tablaTalles = ControladorGeneral.RecuperarTallesPorTipo(codigoTipo);
            grillaTalles.DataSource = tablaTalles;
            grillaTalles.DataBind();
            SetearCaracteristicasComunes(grillaTipos);
        }
        #endregion
    }
}