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
    public partial class GestionAdministradores : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
                if (!IsPostBack)
                {
                    SetearBotones(grillaAdministradores, true, false, false, true, true);
                }
            }
            catch
            {

            }
        }
        private void CargarGrilla()
        {
            DataTable tablaAdmins = ControladorGeneral.RecuperarTodosAdministradores();
            grillaAdministradores.DataSource = tablaAdmins;
            grillaAdministradores.DataBind();
            SetearCaracteristicasComunes(grillaAdministradores);
        }
        protected void grillaAdministradores_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                string contrasena = e.NewValues["contraseña"].ToString();
                string contrasenaRepe = e.NewValues["contraseñaRepetir"].ToString();
                string nombreUsuario = e.NewValues["usuario"].ToString();
                if (!contrasena.Equals(contrasenaRepe))
                {
                    e.Errors[(sender as ASPxGridView).Columns["contraseña"]] = "Las contraseñas deben coincidir";
                }
                if (!ControladorGeneral.ValidarNombreAdministradorEnUso(nombreUsuario))
                {
                    e.Errors[(sender as ASPxGridView).Columns["usuario"]] = "El nombre de usuario ingresado ya se encuentra en uso";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grillaAdministradores_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string contrasena = e.NewValues["contraseña"].ToString();
                string nombreUsuario = e.NewValues["usuario"].ToString();
                ControladorGeneral.InsertarActualizarAdministrador(0, nombreUsuario, contrasena);
                e.Cancel = true;
                grillaAdministradores.CancelEdit();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}