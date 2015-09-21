<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GestionArticulos.aspx.cs" Inherits="Gialo.GestionArticulos" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <script>
        
        function grillaArticulos_EndCallback(s, e) {
            if (s.cpBorrar != undefined && s.cpBorrar != "") {
                alert(s.cpBorrar);
                delete (s.cpBorrar);
            }
        }
    </script>  
    <dx:ASPxGridView ID="grillaArticulos" ClientInstanceName="grid" runat="server"
        KeyFieldName="codigoArticulo" AutoGenerateColumns="False" Width="700px" Theme="Metropolis" 
        OnRowInserting="grillaArticulos_RowInserting" OnRowUpdating="grillaArticulos_RowUpdating"
        OnRowDeleting="grillaArticulos_RowDeleting">
        <Settings ShowFilterRow="true"/>
        <ClientSideEvents EndCallback="function (s, e) {grillaArticulos_EndCallback(s, e);}"/>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoArticulo" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Código">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripción">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True" >
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una descripción" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" VisibleIndex="2" FixedStyle="None">
            </dx:GridViewCommandColumn>--%>
        </Columns>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true" > 
            <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right"/>
        </SettingsPager>
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Artículos" />
    </dx:ASPxGridView>
</asp:Content>

