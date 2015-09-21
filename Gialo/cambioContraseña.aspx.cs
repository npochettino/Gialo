using BibliotecaGiallo.Clases;
using BibliotecaGiallo.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gialo
{
    public partial class cambioContraseña : System.Web.UI.Page
    {
        Administrador oAdministradorActual = new Administrador();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Cargo el form para editar
            txtUsuario.ReadOnly = true;
            int test = int.Parse(Session["codigoAdm"].ToString());
            CargarDatosParaEditar(int.Parse(Session["codigoAdm"].ToString()));
        }

        private void CargarDatosParaEditar(int codigoAdm)
        {

            txtUsuario.ReadOnly = true;
            oAdministradorActual = returnAdministradorActual(codigoAdm);
            Session.Add("administradoActual", oAdministradorActual);
            txtUsuario.Text = oAdministradorActual.NombreUsuario;
        }

        private Administrador returnAdministradorActual(int codigoAdm)
        {
            //busco si el usuario que se quiere crear no exista en la Base de Datos.            
            DataTable dtAdministradores = ControladorGeneral.RecuperarTodosAdministradores();
            Administrador oA = new Administrador();
            for (int i = 0; i < dtAdministradores.Rows.Count; i++)
            {
                int test = int.Parse((dtAdministradores.Rows[i]["idAdministrador"]).ToString());
                if (test == codigoAdm)
                {
                    oA.Codigo = codigoAdm;
                    oA.NombreUsuario = dtAdministradores.Rows[i]["usuario"].ToString();
                    oA.Contraseña = dtAdministradores.Rows[i]["contraseña"].ToString();
                    i = dtAdministradores.Rows.Count;
                }
            }
            return oA;
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                oAdministradorActual = (Administrador)Session["administradoActual"];
                ControladorGeneral.InsertarActualizarAdministrador(oAdministradorActual.Codigo, txtUsuario.Text, txtNewPassword.Text);

                txtOldPassword.Text = "";
                txtNewPassword.Text = "";
                lblPassword.Visible = false;
                pcCambioPassword.ShowOnPageLoad = true;
            }
        }

        private bool validar()
        {
            if (txtOldPassword.Text == "")
            { lblPassword.Visible = true; lblPassword.InnerText = " Debe completar los campos de contraseña."; return false; }
            else if (txtNewPassword.Text == "")
            { lblPassword.Visible = true; lblPassword.InnerText = " Debe completar los campos de contraseña."; return false; }
            else if (txtOldPassword.Text != txtNewPassword.Text)
            { lblPassword.Visible = true; lblPassword.InnerText = " Las contraseñas no coinciden."; return false; }
            else return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}