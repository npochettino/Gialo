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
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    dateInicio.Value = DateTime.Now.Date;
                    dateFin.Value = DateTime.Now.Date.AddDays(1);

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
                }
            }
            catch
            {
            }
        }


        protected void checkTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTodas.Checked)
            {
                dateInicio.ClientEnabled = false;
                dateFin.ClientEnabled = false;
            }
            else
            {
                dateInicio.ClientEnabled = true;
                dateFin.ClientEnabled = true;
            }
        }

        protected void botonGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = Convert.ToDateTime(dateInicio.Value);
                DateTime fechaFin = Convert.ToDateTime(dateFin.Value);
                if (checkTodas.Checked)
                {
                    fechaInicio = DateTime.Now.Date.AddYears(-100);
                    fechaFin = DateTime.Now.Date.AddYears(100);
                }
                int codigoArticulo = Convert.ToInt32(comboArticulos.SelectedValue);

                DataTable Tanda = ControladorGeneral.RecuperarTandasParaReporte(fechaInicio, fechaFin, codigoArticulo);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes/Rdlcs/ReporteTandas.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsTandas", Tanda);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch
            {
            }
        }
    }
}