using BibliotecaGiallo.Controladores;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gialo
{
    public partial class GestionEntregas : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrillaTandas();
                if (!IsPostBack)
                {
                    SetearBotones(grillaTandas, false, false, false, false, false);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        #region Tandas

        private void CargarGrillaTandas()
        {
            DataTable tablaTandas = ControladorGeneral.RecuperarTodasTandas();
            grillaTandas.DataSource = tablaTandas;
            grillaTandas.DataBind();
            SetearCaracteristicasComunes(grillaTandas);
            GridViewColumn a = (grillaTandas.Columns["commnadColumn"] as GridViewColumn);
            if (a != null)
            {
                a.Visible = false;
            }
        }

        #endregion

        #region Entregas

        protected void grillaEntregas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                ASPxGridView grillaEntregas = sender as ASPxGridView;
                int codigoTaller = Convert.ToInt32(e.NewValues["codigoTaller"]);
                DateTime fechaInicio = Convert.ToDateTime(e.NewValues["fechaInicio"]);
                DateTime? fechaFin = Convert.ToDateTime(e.NewValues["fechaFin"]);
                if (fechaFin == Convert.ToDateTime("01/01/0001 00:00:00"))
                {
                    fechaFin = null;
                }
                ControladorGeneral.InsertarActualizarEntrega(0, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()), codigoTaller, fechaInicio, fechaFin);
                e.Cancel = true;
                grillaEntregas.CancelEdit();
                CargarGrillaEntregas(grillaEntregas, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaEntregas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                ASPxGridView grillaEntregas = sender as ASPxGridView;
                int codigo = Convert.ToInt32(e.Keys[0]);
                int codigoTaller = Convert.ToInt32(e.NewValues["codigoTaller"]);
                DateTime fechaInicio = Convert.ToDateTime(e.NewValues["fechaInicio"]);
                DateTime? fechaFin = Convert.ToDateTime(e.NewValues["fechaFin"]);
                if (fechaFin == Convert.ToDateTime("01/01/0001 00:00:00"))
                {
                    fechaFin = null;
                }
                ControladorGeneral.InsertarActualizarEntrega(codigo, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()), codigoTaller, fechaInicio, fechaFin);
                e.Cancel = true;
                grillaEntregas.CancelEdit();
                CargarGrillaEntregas(grillaEntregas, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaEntregas_Load(object sender, EventArgs e)
        {
            ASPxGridView grillaEntregas = sender as ASPxGridView;
            SetearBotones(grillaEntregas, true, true, true, true, true);
            CargarGrillaEntregas(grillaEntregas, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()));
        }

        protected void grillaEntregas_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
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

        protected void grillaEntregas_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //ASPxGridView grilla = (ASPxGridView)sender;
            //string mensaje = ControladorGeneral.VerificarEntregasPendientesPorTanda(Convert.ToInt32(grilla.GetMasterRowKeyValue()));
            //if (mensaje.Equals(string.Empty))
            //{
            //    DataTable tablaTalleres = new DataTable();
            //    tablaTalleres.Columns.Add("codigoTaller");
            //    tablaTalleres.Columns.Add("descripcion");

            //    ((GridViewDataComboBoxColumn)grilla.Columns["codigoTaller"]).PropertiesComboBox.DataSource = tablaTalleres;
            //    ((GridViewDataComboBoxColumn)grilla.Columns["codigoTaller"]).PropertiesComboBox.DataSourceID = null;
            //    //grilla.JSProperties["cpPuedeAgregar"] = "";
            //}
            //else
            //{
            //    grilla.JSProperties["cpPuedeAgregar"] = mensaje;
            //    grilla.CancelEdit();
            //}
            ASPxGridView grilla = (ASPxGridView)sender;
            grilla.CancelEdit();
            Session["codigoTandaCrearEntrega"] = Convert.ToInt32(grilla.GetMasterRowKeyValue());
            Session["codigoEntregaCrearEntrega"] = 0;
            grilla.JSProperties["cpRedirecciona"] = true;
            //Response.Redirect("CrearEntrega.aspx", false);
            //Context.ApplicationInstance.CompleteRequest();
        }

        protected void grillaEntregas_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grilla = (ASPxGridView)sender;
            grilla.CancelEdit();
            Session["codigoTandaCrearEntrega"] = Convert.ToInt32(grilla.GetMasterRowKeyValue());
            Session["codigoEntregaCrearEntrega"] = Convert.ToInt32(e.EditingKeyValue);
            grilla.JSProperties["cpRedirecciona"] = true;
            //Response.Redirect("CrearEntrega.aspx", false);
            //Context.ApplicationInstance.CompleteRequest();
        }


        protected void grillaEntregas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grillaEntregas = sender as ASPxGridView;
            int codigoEntrega = Convert.ToInt32(e.Keys[0]);
            int codigoTanda = Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue());
            if (ControladorGeneral.ValidarEntregaAEliminar(codigoTanda, codigoEntrega))
            {
                ControladorGeneral.EliminarEntrega(codigoTanda, codigoEntrega);
                grillaEntregas.JSProperties["cpBorrar"] = "Se borró la entrega correctamente";
                e.Cancel = true;
                CargarGrillaEntregas(grillaEntregas, Convert.ToInt32(grillaEntregas.GetMasterRowKeyValue()));
            }
            else
            {
                grillaEntregas.JSProperties["cpBorrar"] = "No se puede borrar una entrega cerrada";
                e.Cancel = true;
            }
        }

        private void CargarGrillaEntregas(ASPxGridView grillaEntregas, int codigoTanda)
        {
            try
            {
                DataTable tablaEntregas = ControladorGeneral.RecuperarTodasEntregasPorTanda(codigoTanda);
                grillaEntregas.DataSource = tablaEntregas;
                grillaEntregas.DataBind();
                SetearCaracteristicasComunes(grillaEntregas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaEntregas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.Name.Equals("codigoTaller"))
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.Callback += new CallbackEventHandlerBase(ComboTaller_OnCallback);


                if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;

                combo.DataBind();
            }
        }

        void ComboTaller_OnCallback(object source, CallbackEventArgsBase e)
        {
            FiltrarTalleres(source as ASPxComboBox, e.Parameter);
        }

        private void FiltrarTalleres(ASPxComboBox cmb, string tipo)
        {
            if (!string.IsNullOrEmpty(tipo))
            {
                cmb.Items.Clear();
                DataTable tablaItems = ControladorGeneral.RecuperarTodosTalleresPorTipoTaller(Convert.ToInt32(tipo));
                for (int i = 0; i < tablaItems.Rows.Count; i++)
                {
                    ListEditItem item = new ListEditItem { Value = tablaItems.Rows[i]["codigoTaller"], Text = tablaItems.Rows[i]["descripcion"].ToString() };
                    cmb.Items.Add(item);
                }
            }
        }

        #endregion

    }
}