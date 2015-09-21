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
    public partial class CrearEntrega : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int codigoTanda = Convert.ToInt32(Session["codigoTandaCrearEntrega"]);
            int codigoEntrega = Convert.ToInt32(Session["codigoEntregaCrearEntrega"]);
            if (!IsPostBack)
            {
                if (Session["codigoTandaCrearEntrega"] == null && Convert.ToInt32(Session["codigoTandaCrearEntrega"]) == 0)
                {
                    Response.Redirect("GestionEntregas.aspx");
                }
                if (Session["codigoEntregaCrearEntrega"] != null && Convert.ToInt32(Session["codigoEntregaCrearEntrega"]) != 0)
                {
                    Session["codigoEntregaAuxCrearEntrega"] = Convert.ToInt32(Session["codigoEntregaCrearEntrega"]);
                }
                else
                {
                    Session["codigoEntregaAuxCrearEntrega"] = 0;
                }
                Session["codigoEntregaCrearEntrega"] = 0;


                DataTable tablaTanda = ControladorGeneral.RecuperarTandaPorCodigo(Convert.ToInt32(Session["codigoTandaCrearEntrega"]));
                titulo.Text = "Tanda: " + tablaTanda.Rows[0]["comentario"].ToString() + " - " + "Articulo: " + tablaTanda.Rows[0]["descripcionArticulo"].ToString();

                dateInicio.Value = DateTime.Now.Date;

                DataTable tablaTipoTaller = ControladorGeneral.RecuperarTodosTipoTalleres();
                comboTipoTaller.DataSource = tablaTipoTaller;
                comboTipoTaller.DataTextField = "descripcion";
                comboTipoTaller.DataValueField = "codigoTipoTaller";
                comboTipoTaller.DataBind();

                comboTipoTaller.SelectedIndex = 0;
                CargarTalleres();

                DataTable tablaTandaItems = ControladorGeneral.RecuperarTodosTandaItemsPorTandaDisponibles(codigoTanda);
                DataTable tablaTandaItemsAgregados = tablaTandaItems.Clone();
                if (codigoEntrega != 0)
                {
                    tablaTandaItemsAgregados = ControladorGeneral.RecuperarTandaItemsPorEntrega(codigoEntrega);
                }

                //SAca de la tabla de tanda items de la tanda los que ya estan en la entrega 
                foreach (DataRow filaEnEntrega in tablaTandaItemsAgregados.Rows)
                {
                    for (int i = tablaTandaItems.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToInt32(tablaTandaItems.Rows[i]["codigoTandaItem"]) == Convert.ToInt32(filaEnEntrega["codigoTandaItem"]))
                        {
                            tablaTandaItems.Rows.Remove(tablaTandaItems.Rows[i]);
                            break;
                        }
                    }
                }

                if (Session["codigoEntregaAuxCrearEntrega"] != null && Convert.ToInt32(Session["codigoEntregaAuxCrearEntrega"]) != 0)
                {
                    CargarEntrega(Convert.ToInt32(Session["codigoEntregaAuxCrearEntrega"]));
                }

                Session["tablaTandaItemsCrearEntidad"] = tablaTandaItems;
                CargarGrillaTandaItems(tablaTandaItems);
                Session["tablaTandaItemsAgregadosCrearEntidad"] = tablaTandaItemsAgregados;
                CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
                SetearBotones(grillaTandaItemAgregados, false, true, true, true, true);
            }
            else
            {
                DataTable tablaTandaItems = (DataTable)Session["tablaTandaItemsCrearEntidad"];
                CargarGrillaTandaItems(tablaTandaItems);
                DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
                CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
            }
        }

        private void CargarEntrega(int codigoEntrega)
        {
            try
            {
                DataTable tablaEntrega = ControladorGeneral.RecuperarEntregaPorCodigo(codigoEntrega);
                DataRow fila = tablaEntrega.Rows[0];
                dateInicio.Value = Convert.ToDateTime(fila["fechaInicio"]);
                if (fila["fechaFin"] != DBNull.Value)
                {
                    dateFin.Value = Convert.ToDateTime(fila["fechaFin"]);
                }
                int codigoTipoTaller = Convert.ToInt32(fila["codigoTipoTaller"]);
                int codigoTaller = Convert.ToInt32(fila["codigoTaller"]);
                for (int i = 0; i < comboTipoTaller.Items.Count; i++)
                {
                    if (Convert.ToInt32(comboTipoTaller.Items[i].Value) == codigoTipoTaller)
                    {
                        comboTipoTaller.SelectedIndex = i;
                        break;
                    }
                }
                CargarTalleres(codigoTaller);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void comboTipoTaller_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTalleres();
        }

        private void CargarTalleres()
        {
            int codigoTipoTaller = Convert.ToInt32(comboTipoTaller.SelectedValue);

            DataTable tablaTaller = ControladorGeneral.RecuperarTodosTalleresPorTipoTaller(codigoTipoTaller);

            comboTaller.DataSource = tablaTaller;
            comboTaller.DataTextField = "descripcion";
            comboTaller.DataValueField = "codigoTaller";
            comboTaller.DataBind();
        }

        private void CargarTalleres(int codigoTaller)
        {
            int codigoTipoTaller = Convert.ToInt32(comboTipoTaller.SelectedValue);

            DataTable tablaTaller = ControladorGeneral.RecuperarTodosTalleresPorTipoTaller(codigoTipoTaller);

            comboTaller.DataSource = tablaTaller;
            comboTaller.DataTextField = "descripcion";
            comboTaller.DataValueField = "codigoTaller";
            comboTaller.DataBind();

            for (int i = 0; i < comboTaller.Items.Count; i++)
            {
                if (Convert.ToInt32(comboTaller.Items[i].Value) == codigoTaller)
                {
                    comboTaller.SelectedIndex = i;
                    break;
                }
            }
        }

        private void CargarGrillaTandaItems(DataTable tablaTandaItems)
        {
            //DataTable tablaTandaItems= ControladorGeneral.RecuperarTodosTandaItemsPorTanda(codigoTanda);
            grillaTandaItem.DataSource = tablaTandaItems;
            grillaTandaItem.DataBind();
            //SetearCaracteristicasComunes(grillaTandaItem);
        }

        private void CargarGrillaTandaItemsAgregados(DataTable tablaTandaItems)
        {
            //DataTable tablaTandaItems = ControladorGeneral.RecuperarTandaItemsPorEntrega(codigoEntrega);
            grillaTandaItemAgregados.DataSource = tablaTandaItems;
            grillaTandaItemAgregados.DataBind();
            SetearCaracteristicasComunes(grillaTandaItemAgregados);
            //grillaTandaItemAgregados.Columns[grillaTandaItemAgregados.Columns.Count - 1].Caption = " ";
        }

        protected void grillaTandaItem_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grilla = (ASPxGridView)sender;
            int indice = grilla.EditingRowVisibleIndex;
            int codigoTandaItem = Convert.ToInt32(grilla.GetRowValues(indice, "codigoTandaItem"));

            DataTable tablaTandaItems = (DataTable)Session["tablaTandaItemsCrearEntidad"];
            DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
            for (int i = 0; i < tablaTandaItems.Rows.Count; i++)
            {
                DataRow fila = tablaTandaItems.Rows[i];
                if (Convert.ToInt32(fila["codigoTandaItem"]) == codigoTandaItem)
                {
                    fila["cantidad"] = fila["cantidadDisponible"];
                    tablaTandaItemsAgregados.Rows.Add(fila.ItemArray);
                    tablaTandaItems.Rows[i].Delete();
                    grilla.JSProperties["cpHaceCallBack"] = true;
                    break;
                }
            }
            CargarGrillaTandaItems(tablaTandaItems);
            CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
            e.Cancel = true;
        }

        protected void grillaTandaItemAgregados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grilla = (ASPxGridView)sender;
            //int indice = grilla.EditingRowVisibleIndex;

            int indice = grilla.FindVisibleIndexByKeyValue(e.Keys[grilla.KeyFieldName]);
            int codigoTandaItem = Convert.ToInt32(grilla.GetRowValues(indice, "codigoTandaItem"));
            DataTable tablaTandaItems = (DataTable)Session["tablaTandaItemsCrearEntidad"];
            DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
            for (int i = 0; i < tablaTandaItemsAgregados.Rows.Count; i++)
            {
                DataRow fila = tablaTandaItemsAgregados.Rows[i];
                if (Convert.ToInt32(fila["codigoTandaItem"]) == codigoTandaItem)
                {
                    fila["cantidad"] = fila["cantidadTotal"];
                    tablaTandaItems.Rows.Add(fila.ItemArray);
                    tablaTandaItemsAgregados.Rows[i].Delete();
                    grilla.JSProperties["cpHaceCallBack"] = true;
                    break;
                }
            }
            CargarGrillaTandaItems(tablaTandaItems);
            CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
            e.Cancel = true;
        }

        protected void grillaTandaItemAgregados_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                int cantidad = Convert.ToInt32(e.NewValues["cantidad"]);
                DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
                foreach (DataRow fila in tablaTandaItemsAgregados.Rows)
                {
                    if (Convert.ToInt32(fila["codigoTandaItem"]) == codigo)
                    {
                        int cantidadDisponible = Convert.ToInt32(fila["cantidadDisponible"]);
                        if (cantidad > cantidadDisponible || cantidad < 0)
                        {
                            e.Errors[(sender as ASPxGridView).Columns["cantidad"]] = "La cantidad ingresada no puede superar a la cantidad disponible";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaTandaItemAgregados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.Keys[0]);
                int cantidad = Convert.ToInt32(e.NewValues["cantidad"].ToString());
                e.Cancel = true;

                DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
                foreach (DataRow fila in tablaTandaItemsAgregados.Rows)
                {
                    if (Convert.ToInt32(fila["codigoTandaItem"]) == codigo)
                    {
                        fila["cantidad"] = cantidad;
                    }
                }
                grillaTandaItemAgregados.CancelEdit();
                CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void botonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
                if (tablaTandaItemsAgregados.Rows.Count > 0)
                {
                    int codigoTanda = Convert.ToInt32(Session["codigoTandaCrearEntrega"]);
                    int codigoEntrega = Convert.ToInt32(Session["codigoEntregaAuxCrearEntrega"]);
                    DateTime fechaInicio = Convert.ToDateTime(dateInicio.Value);
                    DateTime? fechaFin = Convert.ToDateTime(dateFin.Value);
                    int codigoTaller = Convert.ToInt32(comboTaller.SelectedValue);
                    if (fechaFin == Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        fechaFin = null;
                    }
                    ControladorGeneral.InsertarActualizarEntrega(codigoEntrega, codigoTanda, codigoTaller, fechaInicio, fechaFin, tablaTandaItemsAgregados);
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                                            "scriptValidacion", "<script>alert('Se guardó la entrega correctamente');window.location.assign('GestionEntregas.aspx');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                                            "scriptValidacion", "<script>alert('Debe agregar al menos un item a la entrega');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                                            "scriptValidacion", "<script>alert('" + ex.Message.ToString() + "');</script>");
            }

        }

        protected void botonAgregarTodos_Click(object sender, EventArgs e)
        {
            DataTable tablaTandaItems = (DataTable)Session["tablaTandaItemsCrearEntidad"];
            DataTable tablaTandaItemsAgregados = (DataTable)Session["tablaTandaItemsAgregadosCrearEntidad"];
            for (int i = tablaTandaItems.Rows.Count - 1; i >= 0; i--)
            {
                DataRow fila = tablaTandaItems.Rows[i];
                fila["cantidad"] = fila["cantidadDisponible"];
                tablaTandaItemsAgregados.Rows.Add(fila.ItemArray);
                tablaTandaItems.Rows[i].Delete();
            }
            CargarGrillaTandaItems(tablaTandaItems);
            CargarGrillaTandaItemsAgregados(tablaTandaItemsAgregados);
        }

        protected void botonVolver_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                            "scriptValidacion", "<script>window.location.assign('GestionEntregas.aspx');</script>");
        }
    }
}