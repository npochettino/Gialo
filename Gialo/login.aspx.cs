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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                msjErrorLogin.Visible = false;
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        protected void txtLogin_Click(object sender, EventArgs e)
        {
            DataTable dtAdministradorActual = ControladorGeneral.RecuperarLogueoAdministrador(txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
            if (dtAdministradorActual != null)
            {
                Session.Add("codigoAdm", dtAdministradorActual.Rows[0][0].ToString());
                Session.Add("usuarioAdm", dtAdministradorActual.Rows[0][1].ToString());
                Session.Add("logueado", true);
                Response.Redirect("index.aspx");
            }
            else
            {
                msjErrorLogin.Visible = true;
            }
        }
    }
}