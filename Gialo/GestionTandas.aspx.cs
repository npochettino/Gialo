using BibliotecaGiallo.Controladores;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
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
    public partial class GestionTandas : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrillaTandas();
                if (!IsPostBack)
                {
                    SetearBotones(grillaTandas, true, true, true, true, true);
                }
            }
            catch
            {

            }
        }

        #region Tandas

        private void CargarGrillaTandas()
        {
            DataTable tablaTandas = ControladorGeneral.RecuperarTodasTandas();
            grillaTandas.DataSource = tablaTandas;
            grillaTandas.DataBind();
            SetearCaracteristicasComunes(grillaTandas);
        }

        protected void grillaTandas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string comentario = e.NewValues["comentario"].ToString();
                int codigoArticulo = Convert.ToInt32(e.NewValues["codigoArticulo"]);
                DateTime fechaInicio = Convert.ToDateTime(e.NewValues["fechaInicio"]);
                DateTime? fechaFin = Convert.ToDateTime(e.NewValues["fechaFin"]);
                if (fechaFin == Convert.ToDateTime("01/01/0001 00:00:00"))
                {
                    fechaFin = null;
                }
                ControladorGeneral.InsertarActualizarTanda(0, comentario, "", fechaInicio, fechaFin, codigoArticulo);
                e.Cancel = true;
                grillaTandas.CancelEdit();
                CargarGrillaTandas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTandas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                string comentario = e.NewValues["comentario"].ToString();
                int codigoArticulo = Convert.ToInt32(e.NewValues["codigoArticulo"]);
                DateTime fechaInicio = Convert.ToDateTime(e.NewValues["fechaInicio"]);
                DateTime? fechaFin = Convert.ToDateTime(e.NewValues["fechaFin"]);
                if (fechaFin == Convert.ToDateTime("01/01/0001 00:00:00"))
                {
                    fechaFin = null;
                }
                ControladorGeneral.InsertarActualizarTanda(codigo, comentario, "", fechaInicio, fechaFin, codigoArticulo);
                e.Cancel = true;
                grillaTandas.CancelEdit();
                CargarGrillaTandas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTandas_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            DateTime fechaInicio = Convert.ToDateTime(e.NewValues["fechaInicio"]);
            DateTime fechaFin = Convert.ToDateTime(e.NewValues["fechaFin"]);
            if (fechaFin != Convert.ToDateTime("01/01/0001 00:00:00"))
            {
                if (fechaFin < fechaInicio)
                {
                    e.Errors[(sender as ASPxGridView).Columns["fechaFin"]] = "La fecha de fin debe ser superior a la fecha de inicio";
                }
            }
        }

        protected void grillaTandas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grillaTanda = sender as ASPxGridView;
            int codigoTanda = Convert.ToInt32(e.Keys[0]);
            if (ControladorGeneral.ValidarTandaAEliminar(codigoTanda))
            {
                ControladorGeneral.EliminarTanda(codigoTanda);
                grillaTanda.JSProperties["cpBorrar"] = "Se borró la tanda correctamente";
                e.Cancel = true;
                CargarGrillaTandas();
            }
            else
            {
                grillaTanda.JSProperties["cpBorrar"] = "No se puede borrar una tanda cerrada";
                e.Cancel = true;
            }
        }

        #endregion

        #region TandaItem

        protected void grillaTandaItem_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            int cantidad = Convert.ToInt32(e.NewValues["cantidad"]);
            if (cantidad == 0)
            {
                e.Errors[(sender as ASPxGridView).Columns["cantidad"]] = "La cantidad no puede ser 0";
            }
            int codigoTalle = Convert.ToInt32(e.NewValues["codigoTalle"]);
            int codigoColor = Convert.ToInt32(e.NewValues["codigoColor"]);
            ASPxGridView grillaTandaItems = sender as ASPxGridView;
            int codigoTanda = Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue());
            int codigoTandaItem;
            if (e.Keys.Count != 0)
            {
                codigoTandaItem = Convert.ToInt32(e.Keys[0]);
            }
            else
            {
                codigoTandaItem = 0;
            }
            if (ControladorGeneral.ValidarTalleEnTanda(codigoTanda, codigoTandaItem, codigoTalle, codigoColor))
            {
                e.Errors[(sender as ASPxGridView).Columns["codigoTalle"]] = "Ya existe un item en la tanda con el mismo talle y color";
            }
        }

        protected void grillaTandaItem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTandaItems = sender as ASPxGridView;
                int codigo = Convert.ToInt32(e.Keys[0]);
                int codigoTalle = Convert.ToInt32(e.NewValues["codigoTalle"]);
                int codigoColor = Convert.ToInt32(e.NewValues["codigoColor"]);
                int cantidad = Convert.ToInt32(e.NewValues["cantidad"]);
                ControladorGeneral.InsertarActualizarTandaItem(codigo, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()), cantidad, codigoTalle, codigoColor);
                e.Cancel = true;
                grillaTandaItems.CancelEdit();
                CargarGrillaTandaItems(grillaTandaItems, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTandaItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                ASPxGridView grillaTandaItems = sender as ASPxGridView;
                int codigoTalle = Convert.ToInt32(e.NewValues["codigoTalle"]);
                int codigoColor = Convert.ToInt32(e.NewValues["codigoColor"]);
                int cantidad = Convert.ToInt32(e.NewValues["cantidad"]);
                ControladorGeneral.InsertarActualizarTandaItem(0, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()), cantidad, codigoTalle, codigoColor);
                e.Cancel = true;
                grillaTandaItems.CancelEdit();
                CargarGrillaTandaItems(grillaTandaItems, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTandaItem_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.Name.Equals("codigoTalle"))
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.Callback += new CallbackEventHandlerBase(ComboTalle_OnCallback);


                if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;

                combo.DataBind();
            }
        }

        void ComboTalle_OnCallback(object source, CallbackEventArgsBase e)
        {
            FiltrarTalles(source as ASPxComboBox, e.Parameter);
        }

        private void FiltrarTalles(ASPxComboBox cmb, string tipo)
        {
            if (!string.IsNullOrEmpty(tipo))
            {
                cmb.Items.Clear();
                DataTable tablaItems= ControladorGeneral.RecuperarTallesPorTipo(Convert.ToInt32(tipo));
                for (int i = 0; i < tablaItems.Rows.Count; i++)
                {
                    ListEditItem item = new ListEditItem { Value = tablaItems.Rows[i]["codigoTalle"], Text = tablaItems.Rows[i]["descripcionTalle"].ToString() };
                    cmb.Items.Add(item);
                }
            }
        }

        protected void grillaTandaItem_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            DataTable tablaTalles = new DataTable();
            tablaTalles.Columns.Add("codigoTalle");
            tablaTalles.Columns.Add("descripcionTalle");

            ASPxGridView grilla = (ASPxGridView)sender;
            ((GridViewDataComboBoxColumn)grilla.Columns["codigoTalle"]).PropertiesComboBox.DataSource = tablaTalles;
            ((GridViewDataComboBoxColumn)grilla.Columns["codigoTalle"]).PropertiesComboBox.DataSourceID = null;
        }

        protected void grillaTandaItem_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            //var grilla = (ASPxGridView)sender;
            //int codigoTanda = Convert.ToInt32(grilla.GetMasterRowKeyValue());
        }

        protected void grillaTandaItem_Load(object sender, EventArgs e)
        {
            ASPxGridView grillaTandaItems = sender as ASPxGridView;
            SetearBotones(grillaTandaItems, true, true, true, true, true);
            CargarGrillaTandaItems(grillaTandaItems, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()));
        }

        protected void grillaTandaItem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grillaTandaItems = sender as ASPxGridView;
            int codigoTandaItem = Convert.ToInt32(e.Keys[0]);
            int codigoTanda = Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue());
            if (ControladorGeneral.ValidarTandaItemAEliminar(codigoTanda, codigoTandaItem))
            {
                ControladorGeneral.EliminarTandaItem(codigoTanda, codigoTandaItem);
                grillaTandaItems.JSProperties["cpBorrar"] = "Se borró el talle de la tanda correctamente";
                e.Cancel = true;
                CargarGrillaTandaItems(grillaTandaItems, Convert.ToInt32(grillaTandaItems.GetMasterRowKeyValue()));
            }
            else
            {
                grillaTandaItems.JSProperties["cpBorrar"] = "No se puede borrar un talle de la tanda cuando pertenece a una entrega";
                e.Cancel = true;
            }
        }

        private void CargarGrillaTandaItems(ASPxGridView grillaTandaItems, int codigoTanda)
        {
            DataTable tablaTandaItems = ControladorGeneral.RecuperarTodosTandaItemsPorTanda(codigoTanda);
            grillaTandaItems.DataSource = tablaTandaItems;
            grillaTandaItems.DataBind();
            SetearCaracteristicasComunes(grillaTandaItems);
        }

        #endregion 

        

       
    }
}