using BibliotecaGiallo.Controladores;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gialo
{
    public partial class ReporteEntregas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable tablaArticulos = ControladorGeneral.RecuperarTodosArticulos();
                    DataRow fila = tablaArticulos.NewRow();
                    fila["codigoArticulo"] = -1;
                    fila["descripcion"] = "TODOS";
                    tablaArticulos.Rows.InsertAt(fila, 0);

                    comboArticulos.DataSource = tablaArticulos;
                    comboArticulos.DataTextField = "descripcion";
                    comboArticulos.DataValueField = "codigoArticulo";
                    comboArticulos.DataBind();

                    comboArticulos.SelectedIndex = 0;

                    ReportViewer1.Visible = false;
                }
            }
            catch
            {
            }
        }

        protected void botonGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewer1.Visible = true;
                int codigoArticulo = Convert.ToInt32(comboArticulos.SelectedValue);

                DataTable entregas = ControladorGeneral.RecuperarEntregasParaReporte(codigoArticulo);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes/Rdlcs/ReporteEntregas.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsEntregas", entregas);
                datasource.Value = entregas;

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                //List<Microsoft.Reporting.WebForms.ReportParameter> paramss = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                //paramss.Add(new Microsoft.Reporting.WebForms.ReportParameter("Texto", "Texto", true));
                //this.ReportViewer1.LocalReport.SetParameters(paramss);

                //ReportParameter p1 = new ReportParameter("Texto", "Texto");
                //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

                ReportViewer1.LocalReport.Refresh();
            }
            catch
            {
            }
        }
    }
}