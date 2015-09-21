using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gialo
{
    public class PaginaBase : System.Web.UI.Page
    {
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    Page.Theme = "Metropolis";
        //}

        public void SetearBotones(ASPxGridView grilla, bool agregar, bool editar, bool eliminar, bool actualizar, bool cancelar)
        {
            GridViewCommandColumn columna = new GridViewCommandColumn();
            if (agregar)
            {
                columna.ShowNewButtonInHeader = true;
                grilla.SettingsCommandButton.NewButton.Text = "<i class=\"ion-plus-round\"></i>";
            }
            if (editar)
            {
                columna.ShowEditButton = true;
                grilla.SettingsCommandButton.EditButton.Text = "<i class=\"ion-edit\"></i>";
            }
            if (eliminar)
            {
                columna.ShowDeleteButton = true;
                grilla.SettingsCommandButton.DeleteButton.Text = "<i style=\"margin-left: 15px\" class=\"ion-minus-round\"></i>";
            }
            if (actualizar)
            {
                grilla.SettingsCommandButton.UpdateButton.Text = "<i class=\"ion-checkmark-round\"></i>";
            }
            if (cancelar)
            {
                grilla.SettingsCommandButton.CancelButton.Text = "<i class=\"ion-close-round\"></i>";
            }
            grilla.Columns.Add(columna);
        }
        public void SetearCaracteristicasComunes(ASPxGridView grilla)
        {
            grilla.Font.Size = 10;
        }
    }
}
